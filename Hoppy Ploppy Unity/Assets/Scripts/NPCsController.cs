using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public enum NPCMode
{
    Wonder, MoveAway
}
public class NPCsController : MonoBehaviour
{
    GameController gameController;
    NavMeshAgent agent;
    public float persistance;
    public float smellFreqnecy;
    Vector3 investigationZone;
    public float investigateZoneSize;
    public NPCMode mode;
    public float maxDistanceToStopChasing = 15f;
    float cacheAgentSpeed = 0f;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        StartCoroutine(MoveRandomPlace());
        StartCoroutine(Smell());
        mode = NPCMode.Wonder;
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Smell()
    {
        while (true)
        {
            var smelliness = PoopSpread.PoopSpreadGrid
                [Mathf.FloorToInt(transform.position.x)]
                [Mathf.FloorToInt(transform.position.z)];
            if (smelliness > 1)
            {
                cacheAgentSpeed = agent.speed;
                agent.speed = agent.speed / 2;
            }
            else if (cacheAgentSpeed != 0)
            {
                agent.speed = cacheAgentSpeed;
                cacheAgentSpeed = 0f;
            }
            if (smelliness > 0.1 && mode != NPCMode.MoveAway)
            {
                mode = NPCMode.Wonder;
                investigationZone = transform.position;
                WaitAndStopMoveAway();
            }
            yield return new WaitForSeconds(smellFreqnecy);
        }
    }

    IEnumerator WaitAndStopMoveAway()
    {
        yield return new WaitForSeconds(persistance);
        if (mode == NPCMode.MoveAway)
        {
            mode = NPCMode.Wonder;
        }
    }

    IEnumerator MoveRandomPlace()
    {
        var newXPos = 0f;
        var newYPos = 0f;
        while (true)
        {
            switch (mode)
            {
                case NPCMode.Wonder:
                    animator.SetBool("Walk", true);
                    animator.SetBool("Run", false);
                    //Debug.Log("NPC Wonder");
                    newXPos = Random.Range(0, gameController.mapXSize);
                    newYPos = Random.Range(0, gameController.mapYSize);
                    //Debug.Log($"NPC going to position ({newXPos},{newYPos})");
                    agent.destination = new Vector3(newXPos, 0, newYPos);
                    var startWonderTime = Time.time;
                    while (Time.time - startWonderTime < persistance)
                    {
                        if (mode != NPCMode.Wonder) break;
                        yield return null;
                    }
                    break;
                case NPCMode.MoveAway:
                    animator.SetBool("Walk", false);
                    animator.SetBool("Run", true);
                    //Debug.Log("Move Away");
                    newXPos = Random.Range(
                        Mathf.Min(0, investigationZone.x - investigateZoneSize),
                        Mathf.Max(investigationZone.x + investigateZoneSize));
                    newYPos = Random.Range(
                        Mathf.Min(0, investigationZone.y - investigateZoneSize),
                        Mathf.Max(investigationZone.y + investigateZoneSize));
                    //Debug.Log($"NPC Move Away to position ({newXPos},{newYPos})");
                    agent.destination = new Vector3(newXPos, 0, newYPos);
                    while (agent.remainingDistance > 0.3)
                    {
                        yield return null;
                    }
                    break;
            }
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        if (agent == null) return;
        if (mode == NPCMode.MoveAway)
        {
            Color c = Color.red;
            c.a = 0.2f;
            Gizmos.color = c;

            Gizmos.DrawSphere(investigationZone, investigateZoneSize);
        }
        if (mode == NPCMode.Wonder)
        {
            Color c = Color.blue;
            c.a = 1f;
            Gizmos.color = c;

            Gizmos.DrawSphere(agent.destination, 2);
        }
    }
}

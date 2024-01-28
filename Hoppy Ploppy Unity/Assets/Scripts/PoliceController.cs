using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public enum PoliceMode
{
    Wonder, Investigate, Chase
}
public class PoliceController : MonoBehaviour
{
    GameController gameController;
    NavMeshAgent agent;
    public float persistance;
    public float smellFreqnecy;
    Vector3 investigationZone;
    public float investigateZoneSize;
    public PoliceMode mode;
    public float maxDistanceToStopChasing = 15f;
    float cacheAgentSpeed = 0f;
    Animator animator;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        StartCoroutine(MoveRandomPlace());
        StartCoroutine(Smell());
        mode = PoliceMode.Wonder;

        player = FindObjectOfType<CharacterController>().gameObject;
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
            if (smelliness > 0.1 && mode != PoliceMode.Chase)
            {
                mode = PoliceMode.Investigate;
                investigationZone = transform.position;
                WaitAndStopInvestigating();
            }
            yield return new WaitForSeconds(smellFreqnecy);
        }
    }

    IEnumerator WaitAndStopInvestigating()
    {
        yield return new WaitForSeconds(persistance);
        if (mode == PoliceMode.Investigate)
        {
            mode = PoliceMode.Wonder;
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
                case PoliceMode.Wonder:
                    animator.SetBool("Walk", true);
                    animator.SetBool("Investigate", false);
                    animator.SetBool("Chase", false);
                    //Debug.Log("Police Wonder");
                    newXPos = Random.Range(0, gameController.mapXSize);
                    newYPos = Random.Range(0, gameController.mapYSize);
                    //Debug.Log($"Police going to position ({newXPos},{newYPos})");
                    agent.destination = new Vector3(newXPos, 0, newYPos);
                    var startWonderTime = Time.time;
                    while (Time.time - startWonderTime < persistance)
                    {
                        if (agent.remainingDistance < 0.3)
                        {
                            animator.SetBool("Walk", false);
                            animator.SetBool("Investigate", false);
                            animator.SetBool("Chase", false);
                        }
                        if (mode != PoliceMode.Wonder) break;
                        yield return null;
                    }
                    break;
                case PoliceMode.Investigate:
                    animator.SetBool("Walk", false);
                    animator.SetBool("Investigate", true);
                    animator.SetBool("Chase", false);
                    //Debug.Log("Police Investigate");
                    newXPos = Random.Range(
                        Mathf.Max(0, investigationZone.x - investigateZoneSize), 
                        Mathf.Min(investigationZone.x + investigateZoneSize));
                    newYPos = Random.Range(
                        Mathf.Max(0, investigationZone.y - investigateZoneSize),
                        Mathf.Min(investigationZone.y + investigateZoneSize));
                    //Debug.Log($"Police investigate to position ({newXPos},{newYPos})");
                    agent.destination = new Vector3(newXPos, 0, newYPos);
                    while (agent.remainingDistance > 0.3)
                    {
                        yield return null;
                    }
                    break;
                case PoliceMode.Chase:
                    animator.SetBool("Walk", false);
                    animator.SetBool("Investigate", false);
                    animator.SetBool("Chase", true);
                    //Debug.Log("Police Chase");
                    agent.destination = player.transform.position;
                    while (agent.remainingDistance < maxDistanceToStopChasing)
                    {
                        agent.destination = player.transform.position;
                        if (mode != PoliceMode.Chase) break;
                        yield return new WaitForSeconds(0.5f);
                    }
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        if (agent == null) return;
        if (mode == PoliceMode.Investigate)
        {
            Color c = Color.red;
            c.a = 0.2f;
            Gizmos.color = c;

            Gizmos.DrawSphere(investigationZone, investigateZoneSize);
        }
        if (mode == PoliceMode.Wonder)
        {
            Color c = Color.blue;
            c.a = 1f;
            Gizmos.color = c;

            Gizmos.DrawSphere(agent.destination, 2);
        }
        if (mode == PoliceMode.Chase)
        {
            Color c = Color.red;
            c.a = 1f;
            Gizmos.color = c;

            Gizmos.DrawSphere(agent.destination, 2);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceController : MonoBehaviour
{
    GameController gameController;
    NavMeshAgent agent;
    public float t;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        StartCoroutine(MoveRandomPlace());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator MoveRandomPlace()
    {
        while (true)
        {
            var newXPos = Random.Range(0, gameController.mapXSize);
            var newYPos = Random.Range(0, gameController.mapYSize);
            Debug.Log($"Police going to position ({newXPos},{newYPos})");
            agent.destination = new Vector3(newXPos, 0, newYPos);
            yield return new WaitForSeconds(t);
        }
    }
}

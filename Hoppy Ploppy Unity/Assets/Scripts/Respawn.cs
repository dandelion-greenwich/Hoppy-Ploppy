using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.position = new Vector3 (3, 10, 3);
        }
    }
}

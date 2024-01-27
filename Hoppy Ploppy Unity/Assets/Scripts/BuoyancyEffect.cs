using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyancyEffect : MonoBehaviour
{
    Rigidbody rb;
    
    [SerializeField] float buoyancyLevel, damping;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerStay(Collider other)
    {
        //Debug.Log("owo");
        if (other.gameObject.name == "Lake")
        {
            rb.AddForce(Vector3.up * rb.mass + new Vector3(0,buoyancyLevel, 0));
            rb.AddForce(-rb.velocity * rb.mass * damping);
            //Debug.Log("OwO");

        }
    }
}

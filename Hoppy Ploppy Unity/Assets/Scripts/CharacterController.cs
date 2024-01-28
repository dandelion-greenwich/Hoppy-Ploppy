using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float Speed = 10;
    CameraController cameraController;
    public float JumpHeight;

    public bool jumping, climbing, treeColl;

    public float shitMeter;

    public List<GameObject> shit;
    Animator animiator;
    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        cameraController = cam.GetComponent<CameraController>();
        animiator = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.EulerAngles(0, cameraController.angleAroundCharacter, 0);
        CollidingWithTree();
        CaptureInput();
    }

    void CaptureInput()
    {
        if (Input.GetKey(KeyCode.W)) {
            if (!climbing)
            {
                var pos = transform.position;
                pos -= transform.forward * Speed * Time.deltaTime;
                transform.position = pos;
                animiator.SetBool("Walk", true);
                animiator.SetBool("Jump", false);
                animiator.SetBool("Poo", false);
                animiator.SetBool("Climb", false);
            }
            else
            {
                var pos = transform.position;
                pos += transform.up * Speed * Time.deltaTime;
                transform.position = pos;
                animiator.SetBool("Walk", false);
                animiator.SetBool("Jump", false);
                animiator.SetBool("Poo", false);
                animiator.SetBool("Climb", true);
            }
        }
        else if (Input.GetKey(KeyCode.S)) {
            if (!climbing)
            {
                var pos = transform.position;
                pos += transform.forward * Speed * Time.deltaTime;
                transform.position = pos;
                animiator.SetBool("Walk", true);
                animiator.SetBool("Jump", false);
                animiator.SetBool("Poo", false);
                animiator.SetBool("Climb", false);
            }
            else
            {
                var pos = transform.position;
                pos -= transform.up * Speed * Time.deltaTime;
                transform.position = pos;
                animiator.SetBool("Walk", false);
                animiator.SetBool("Jump", false);
                animiator.SetBool("Poo", false);
                animiator.SetBool("Climb", true);
            }
        }
        if (Input.GetKey(KeyCode.A)) {
            var pos = transform.position;
            pos += transform.right * Speed * Time.deltaTime;
            transform.position = pos;
            animiator.SetBool("Walk", true);
            animiator.SetBool("Jump", false);
            animiator.SetBool("Poo", false);
            animiator.SetBool("Climb", false);
        }
        else if (Input.GetKey(KeyCode.D)) {
            var pos = transform.position;
            pos -= transform.right * Speed * Time.deltaTime;
            transform.position = pos;
            animiator.SetBool("Walk", true);
            animiator.SetBool("Jump", false);
            animiator.SetBool("Poo", false);
            animiator.SetBool("Climb", false);
        }

        if (Input.GetKey(KeyCode.Space) && jumping == false)
        {
            jumping = true;
            GetComponent<Rigidbody>().AddForce(new Vector3(0, JumpHeight * 100, 0));
            animiator.SetBool("Walk", false);
            animiator.SetBool("Jump", true);
            animiator.SetBool("Poo", false);
            animiator.SetBool("Climb", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shit(1);
            animiator.SetBool("Walk", false);
            animiator.SetBool("Jump", false);
            animiator.SetBool("Poo", true);
            animiator.SetBool("Climb", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            jumping = false;
            treeColl = false;
            climbing = false;
        }        

        if (collision.gameObject.tag == "Police")
        {
            Debug.Log("Loose");
            Application.Quit();
        }

        if (collision.gameObject.tag == "Tree" && !climbing)
        {
            climbing = true;
            treeColl = true;
            jumping = false;
            Debug.Log("climbing");
        }
        
        
        if(collision == null || collision.gameObject.tag != "Tree")
        {
            jumping = false;
            climbing = false;
            treeColl = false;
        }       
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    void CollidingWithTree()
    {
        if (!treeColl && !climbing)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;           
        }
        else
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    void Shit(float ammount)
    {
        if (shitMeter >= ammount)
        {
            shitMeter -= ammount;
            if (Physics.Raycast(transform.position, -transform.up, out var hit, 20f) == false) { return; }
            //Debug.Log($"Shitting on {hit.transform.name}");
            var type = Random.Range(0, shit.Count);
            Instantiate(shit[type], new Vector3(transform.position.x, hit.point.y, transform.position.z), Quaternion.identity, null);
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food" && shitMeter < 100)
        {
            Destroy(other.gameObject);
            shitMeter++;
        }

    }
}
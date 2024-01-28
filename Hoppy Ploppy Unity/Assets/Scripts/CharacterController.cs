using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float Speed = 10;
    CameraController cameraController;
    public float JumpHeight;

    public bool jumping, climbing, treeColl;

    public float shitMeter;

    public GameObject shit;
    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        cameraController = cam.GetComponent<CameraController>();
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
            }
            else
            {
                var pos = transform.position;
                pos += transform.up * Speed * Time.deltaTime;
                transform.position = pos;
            }
        }
        else if (Input.GetKey(KeyCode.S)) {
            if (!climbing)
            {
                var pos = transform.position;
                pos += transform.forward * Speed * Time.deltaTime;
                transform.position = pos;                
            }
            else
            {
                var pos = transform.position;
                pos -= transform.up * Speed * Time.deltaTime;
                transform.position = pos;
            }
        }
        if (Input.GetKey(KeyCode.A)) {
            var pos = transform.position;
            pos += transform.right * Speed * Time.deltaTime;
            transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.D)) {
            var pos = transform.position;
            pos -= transform.right * Speed * Time.deltaTime;
            transform.position = pos;
        }

        if (Input.GetKey(KeyCode.Space) && jumping == false)
        {
            jumping = true;
            GetComponent<Rigidbody>().AddForce(new Vector3(0, JumpHeight * 100, 0));
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shit(1);
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
            Instantiate(shit, new Vector3(transform.position.x, hit.point.y, transform.position.z), Quaternion.identity, null);
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float Speed = 10;
    CameraController cameraController;
    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        cameraController = cam.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        CaptureInput();
    }

    void CaptureInput()
    {
        if (Input.GetKey(KeyCode.W)) {
            var pos = transform.position;
            pos.z += Speed * Time.deltaTime;
            transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.S)) {
            var pos = transform.position;
            pos.z -= Speed * Time.deltaTime;
            transform.position = pos;
        }
        if (Input.GetKey(KeyCode.A)) {
            var pos = transform.position;
            pos.x -= Speed * Time.deltaTime;
            transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.D)) {
            var pos = transform.position;
            pos.x += Speed * Time.deltaTime;
            transform.position = pos;
        }
    }
}
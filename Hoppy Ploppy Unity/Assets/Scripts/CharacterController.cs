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
        transform.rotation = Quaternion.EulerAngles(0, cameraController.angleAroundCharacter, 0);
        CaptureInput();
    }

    void CaptureInput()
    {
        if (Input.GetKey(KeyCode.W)) {
            var pos = transform.position;
            pos -= transform.forward * Speed * Time.deltaTime;
            transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.S)) {
            var pos = transform.position;
            pos += transform.forward * Speed * Time.deltaTime;
            transform.position = pos;
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
    }
}
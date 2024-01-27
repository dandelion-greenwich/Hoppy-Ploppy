using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject target;
    public float angleAroundCharacter;
    [SerializeField] float distance;
    [SerializeField] float height;
    [SerializeField] float speed;
    Vector3 mousePosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angleAroundCharacter += GetInput();
        float relateiveX = distance * Mathf.Sin(angleAroundCharacter);
        float relateiveY = distance * Mathf.Cos(angleAroundCharacter);
        Vector3 targetPos = target.transform.position;
        transform.position = new Vector3(targetPos.x + relateiveX, height, targetPos.z + relateiveY);
        transform.LookAt(targetPos);
    }

    float GetInput()
    {
        var tmpMousePos = Input.mousePosition;
        var mouseDiffX = tmpMousePos.x - mousePosition.x;
        mousePosition = tmpMousePos;
        //var mouseDiffY = tmpMousePos.y - mousePosition.y;
        Debug.Log(mouseDiffX);

        if (Input.GetMouseButton(0) == false) return 0f;

        return mouseDiffX * speed;
    }
}

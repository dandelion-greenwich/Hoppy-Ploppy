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
    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;
    [SerializeField] float speed;
    Vector3 mousePosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mouseDiff = GetInput();
        angleAroundCharacter += mouseDiff.x;
        height += mouseDiff.y;
        float relateiveX = distance * Mathf.Sin(angleAroundCharacter);
        float relateiveY = distance * Mathf.Cos(angleAroundCharacter);
        Vector3 targetPos = target.transform.position;
        transform.position = new Vector3(targetPos.x + relateiveX, height, targetPos.z + relateiveY);
        transform.LookAt(targetPos);
    }

    Vector2 GetInput()
    {
        var tmpMousePos = Input.mousePosition;
        var mouseDiffX = tmpMousePos.x - mousePosition.x;
        var mouseDiffY = tmpMousePos.y - mousePosition.y;
        mousePosition = tmpMousePos;
        //if (Input.GetMouseButton(0) == false) return new Vector2();

        return new Vector2(mouseDiffX * speed, -mouseDiffY * speed);
    }
}

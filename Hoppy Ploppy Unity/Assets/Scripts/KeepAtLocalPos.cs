using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAtLocalPos : MonoBehaviour
{
    Vector3 localPos;
    Quaternion localRotation;
    // Start is called before the first frame update
    void Start()
    {
        localPos = transform.localPosition;
        localRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = localPos;
        transform.localRotation = localRotation;
    }
}

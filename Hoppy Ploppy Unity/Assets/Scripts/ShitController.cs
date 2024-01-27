using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitController : MonoBehaviour
{
    public float courics = 1;
    public float pungance = 1;
    // Start is called before the first frame update
    void Start()
    {
        PoopSpread.SpreadPoop(transform.position.x, transform.position.z, courics, pungance);
    }
}

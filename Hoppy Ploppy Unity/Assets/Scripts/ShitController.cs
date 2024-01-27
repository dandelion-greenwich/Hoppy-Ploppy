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
        var police = FindObjectsOfType<PoliceController>();
        foreach (var p in police)
        {
            //Debug.Log($"Just shat, checking p {p.name} is close");
            if (Vector3.Magnitude(p.transform.position - transform.position) < p.maxDistanceToStopChasing)
            {
                Debug.Log($"p {p.name} is close, causing chase");
                p.mode = PoliceMode.Chase;
            }
        }
    }
}

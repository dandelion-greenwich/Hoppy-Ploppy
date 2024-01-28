using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitController : MonoBehaviour
{
    public float courics = 1;
    public float pungance = 1;
    public GameObject grass;

    // Start is called before the first frame update
    void Start()
    {
        PoopSpread.SpreadPoop(transform.position.x, transform.position.z, courics, pungance, AddGrass);
        var police = FindObjectsOfType<PoliceController>();
        foreach (var p in police)
        {
            //Debug.Log($"Just shat, checking p {p.name} is close");
            if (Vector3.Magnitude(p.transform.position - transform.position) < p.maxDistanceToStopChasing)
            {
                p.mode = PoliceMode.Chase;
            }
        }
    }

    void AddGrass(int x, int y)
    {
        Instantiate(grass, new Vector3((float)x, 0.8f, (float)y), new Quaternion());
    }
}

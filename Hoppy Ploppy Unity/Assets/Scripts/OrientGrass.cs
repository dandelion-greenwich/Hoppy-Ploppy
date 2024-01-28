using Unity.Burst.CompilerServices;
using UnityEngine;

public class OrientGrass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(0, -10, 0);
        var hitsBelow = Physics.RaycastAll(transform.position + new Vector3(0,20,0), Vector3.down, 40f);
        foreach (var hit in hitsBelow)
        {
            if (hit.transform.tag == "Terrain")
            {
                transform.rotation = Quaternion.Euler(hit.normal.x * Mathf.Rad2Deg, hit.normal.y * Mathf.Rad2Deg, hit.normal.z * Mathf.Rad2Deg);
                transform.position = hit.point - new Vector3(0,0.4f,0);
                return;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCompletion : MonoBehaviour
{
    RectTransform rt;
    float initYSize;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        initYSize = rt.sizeDelta.y;
    }

    // Update is called once per frame
    void Update()
    {
        //rt.sizeDelta = new Vector2(rt.sizeDelta.y, PoopSpread.count / PoopSpread.mapSize * 100);
        rt.sizeDelta = new Vector2((float)PoopSpread.count / (float)PoopSpread.mapSize * 100, initYSize);
    }
}

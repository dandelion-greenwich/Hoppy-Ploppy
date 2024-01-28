using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooBar : MonoBehaviour
{
    public CharacterController controller;
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
        rt.sizeDelta = new Vector2(controller.shitMeter, initYSize);
    }
}

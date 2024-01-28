using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompletionPercentText : MonoBehaviour
{
    TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = $"{(int)((float)PoopSpread.count / (float)PoopSpread.mapSize * 100)}%";
    }
}

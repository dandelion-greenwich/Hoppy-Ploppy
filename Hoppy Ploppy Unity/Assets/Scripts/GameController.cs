using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int mapXSize = 10;
    public int mapYSize = 10;

    // Start is called before the first frame update
    void Awake()
    {
        PoopSpread.PoopSpreadGrid = new float[mapXSize][];
        for (int i = 0; i < mapXSize; i++)
        {
            PoopSpread.PoopSpreadGrid[i] = new float[mapYSize];
        }
    }
}

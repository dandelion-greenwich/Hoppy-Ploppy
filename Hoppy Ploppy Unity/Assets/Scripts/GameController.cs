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

    private void OnDrawGizmos()
    {
        if (PoopSpread.PoopSpreadGrid == null) return;
        var c = Color.grey;
        c.a = 0.2f;
        Gizmos.color = c;
        for (int x = 0; x < mapXSize; x++)
        {
            for (int y = 0;  y < mapYSize; y++)
            {
                var poopVal = PoopSpread.PoopSpreadGrid[x][y];
                if (poopVal > 0)
                {
                    c = Color.green;
                    c.a = PoopSpread.PoopSpreadGrid[x][y];
                    Gizmos.color = c;
                }
                else
                {
                    c = Color.grey;
                    c.a = 0.2f;
                    Gizmos.color = c;
                }
                Gizmos.DrawCube(new Vector3(x, 0, y), new Vector3(1, 1, 1));
            }
        }
        
    }
}

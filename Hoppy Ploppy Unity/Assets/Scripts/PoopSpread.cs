using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PoopSpread
{
    public float[][] PoopSpreadGrid;
    int xLength => PoopSpreadGrid.Length;
    int yLength => PoopSpreadGrid[0].Length;

    void SpreadPoop(int xpos, int ypos, int radius, float size) {
        for (int x = 0; x < xLength; x++)
        {
            for (int y = 0; y < yLength; y++)
            {
                float distance = Mathf.Sqrt(x*x + y*y);
                if (distance <= radius) {
                    PoopSpreadGrid[x][y] += size;
                }
            }
        }
    }
}
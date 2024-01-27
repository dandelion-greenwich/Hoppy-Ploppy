using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PoopSpread
{
    static public float[][] PoopSpreadGrid;
    static int xLength => PoopSpreadGrid.Length;
    static int yLength => PoopSpreadGrid[0].Length;

    public static void SpreadPoop(float xpos, float ypos, float courics, float pungance) { // courics is radius of spread, pungance is strength
        for (int x = 0; x < xLength; x++)
        {
            for (int y = 0; y < yLength; y++)
            {
                var xdiff = x - xpos;
                var ydiff = y - ypos;
                float distance = Mathf.Sqrt(xdiff*xdiff + ydiff*ydiff);
                if (distance <= courics) {
                    PoopSpreadGrid[x][y] += pungance;
                }
            }
        }
    }

    public static void RemovePoop(int xpos, int ypos, float courics, float pungance)
    {
        for (int x = 0; x < xLength; x++)
        {
            for (int y = 0; y < yLength; y++)
            {
                var xdiff = x - xpos;
                var ydiff = y - ypos;
                float distance = Mathf.Sqrt(xdiff * xdiff + ydiff * ydiff);
                if (distance <= courics)
                {
                    PoopSpreadGrid[x][y] -= pungance;
                }
            }
        }
    }
}
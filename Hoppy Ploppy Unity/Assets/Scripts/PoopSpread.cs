using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PoopSpread
{
    static public float[][] PoopSpreadGrid;
    static int xLength => PoopSpreadGrid.Length;
    static int yLength => PoopSpreadGrid[0].Length;
    static int count;

    public static void SpreadPoop(float xpos, float ypos, float courics, float pungance) { // courics is radius of spread, pungance is strength
        for (int x = Mathf.Max(0,Mathf.FloorToInt(xpos-courics-1)); x < Mathf.Min(Mathf.CeilToInt(xpos + courics + 1), xLength); x++)
        {
            for (int y = Mathf.Max(0,Mathf.FloorToInt(ypos - courics - 1)); y < Mathf.Min(yLength, Mathf.CeilToInt(ypos + courics + 1)); y++)
            {
                var xdiff = x - xpos;
                var ydiff = y - ypos;
                float distance = Mathf.Sqrt(xdiff*xdiff + ydiff*ydiff);
                if (distance <= courics) {
                    try
                    {
                        if (pungance == 0) 
                        { 
                            count++;
                            Debug.Log(count);
                        }
                        PoopSpreadGrid[x][y] += pungance;
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning($"Uh oh, you are trying to shit outside the map!! coords: ({x},{y})");
                    }
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
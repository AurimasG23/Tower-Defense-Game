using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingDimensions
{
    public int xLength;
    public int zWidth;

    public BuildingDimensions(int length, int width)
    {
        xLength = length;
        zWidth = width;
    }
}

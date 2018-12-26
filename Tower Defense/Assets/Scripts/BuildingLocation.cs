using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingLocation
{
    public float x;    
    public float y;
    public float z;

    public BuildingLocation(float _x, float _y, float _z)
    {
        x = _x;
        y = _y;
        z = _z;
    }

}

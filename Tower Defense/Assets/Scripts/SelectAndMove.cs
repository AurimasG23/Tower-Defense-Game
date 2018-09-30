using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAndMove : MonoBehaviour
{
    public GameObject[] buildings;
    private static int numberOfBuildings = 3;
    public int selectedBuildingIndex;

    private MeshRenderer[] cubeMeshRenderers = new MeshRenderer[numberOfBuildings];
    public Material red;
    public Material green;

    public static SelectAndMove instance;
	// Use this for initialization
	void Start ()
    {
        instance = this;

        for(int i = 0; i < numberOfBuildings; i++)
        {
            cubeMeshRenderers[i] = buildings[i].GetComponent<MeshRenderer>();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void selectBuilding(int index)
    {
        cubeMeshRenderers[selectedBuildingIndex].material = red;
        selectedBuildingIndex = index;
        cubeMeshRenderers[selectedBuildingIndex].material = green;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAndMove : MonoBehaviour
{
    public GameObject[] buildings;                      //pastatai
    //public GameObject[] buildingsObjects;               //pastatu objektai (virsutine dalis)
    //public GameObject[] buildingsBasis;                 //pastatu pagrindai (plokscia apacia)
    private static int numberOfBuildings = 2;           //pastatu kiekis
    public BuildingDimensions[] buildingsDimensions;    //pastatu matmenys
    public int selectedBuildingIndex = -1;

    //private MeshRenderer[] buildingObjectsMeshRenderers = new MeshRenderer[numberOfBuildings];
    private MeshRenderer[] buildingBasisMeshRenderers = new MeshRenderer[numberOfBuildings];
    public Material red;
    public Material green;

    public static SelectAndMove instance;
	// Use this for initialization
	void Start ()
    {
        instance = this;

        for(int i = 0; i < numberOfBuildings; i++)
        {
            //cubeMeshRenderers[i] = buildings[i].GetComponent<MeshRenderer>();
            //buildingBasisMeshRenderers[i] = buildingsBasis[i].GetComponent<MeshRenderer>();
        }

        selectedBuildingIndex = -1;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void selectBuilding(int index)
    {
        if (selectedBuildingIndex != -1)
        {
            //cubeMeshRenderers[selectedBuildingIndex].material = red;
        }
        selectedBuildingIndex = index;
        //cubeMeshRenderers[selectedBuildingIndex].material = green;
        BuildingPlacement.instance.SetItem(buildings[selectedBuildingIndex]);
    }

    public void DeselectBuildings()
    {
        if (selectedBuildingIndex != -1)
        {
            //cubeMeshRenderers[selectedBuildingIndex].material = red;
            selectedBuildingIndex = -1;
        }
    }
}

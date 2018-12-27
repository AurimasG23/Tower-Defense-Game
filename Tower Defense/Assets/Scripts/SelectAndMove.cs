using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAndMove : MonoBehaviour
{
    //------------------------------------------------------------------
    private static int numberOfBuildings = 4;           //pastatu kiekis
    //------------------------------------------------------------------

    public GameObject[] buildings;                      //pastatai
    //public GameObject[] buildingsObjects;             //pastatu objektai (virsutine dalis)
    //public GameObject[] buildingsBasis;               //pastatu pagrindai (plokscia apacia)   
    public BuildingDimensions[] buildingsDimensions;    //pastatu matmenys
    public int selectedBuildingIndex = -1;

    private BuildingLocation[] buildingsLocations = new BuildingLocation[numberOfBuildings];
    private string buildingLocationsDataFile = "buildingLocations";

    //private MeshRenderer[] buildingObjectsMeshRenderers = new MeshRenderer[numberOfBuildings];
    private MeshRenderer[] buildingBasisMeshRenderers = new MeshRenderer[numberOfBuildings];
    public Material red;
    public Material green;

    public static SelectAndMove instance;
	// Use this for initialization
	void Start ()
    {
        instance = this;

        //for(int i = 0; i < numberOfBuildings; i++)
        //{
            //cubeMeshRenderers[i] = buildings[i].GetComponent<MeshRenderer>();
            //buildingBasisMeshRenderers[i] = buildingsBasis[i].GetComponent<MeshRenderer>();
        //}

        selectedBuildingIndex = -1;

        DataFileHandler.SetBuildingsLocationsOnFirstLaunch(buildingLocationsDataFile, numberOfBuildings);
        buildingsLocations = DataFileHandler.ReadBuildingLocations(buildingLocationsDataFile, numberOfBuildings);
        for (int i = 0; i < numberOfBuildings; i++)
        {
            buildings[i].transform.position = new Vector3(buildingsLocations[i].x, buildingsLocations[i].y, buildingsLocations[i].z);
        }
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
            buildingsLocations[selectedBuildingIndex] = new BuildingLocation(buildings[selectedBuildingIndex].transform.position.x, buildings[selectedBuildingIndex].transform.position.y, buildings[selectedBuildingIndex].transform.position.z);
        }
        selectedBuildingIndex = index;
        //cubeMeshRenderers[selectedBuildingIndex].material = green;
        BuildingPlacement.instance.SetItem(buildings[selectedBuildingIndex], buildingsDimensions[selectedBuildingIndex]);
    }

    public void DeselectBuildings()
    {
        if (selectedBuildingIndex != -1)
        {
            //cubeMeshRenderers[selectedBuildingIndex].material = red;
            buildingsLocations[selectedBuildingIndex] = new BuildingLocation(buildings[selectedBuildingIndex].transform.position.x, buildings[selectedBuildingIndex].transform.position.y, buildings[selectedBuildingIndex].transform.position.z);            
            selectedBuildingIndex = -1;
        }
    }

    public void SaveBuildingsLocations()
    {
        //buildingsLocations[0] = new BuildingLocation(8, 0, 8);
        //buildingsLocations[1] = new BuildingLocation(-8, 0, 8);
        //buildingsLocations[2] = new BuildingLocation(-8, 0, -8);
        //buildingsLocations[3] = new BuildingLocation(8, 0, -8);
        DataFileHandler.ChangeBuildingLocations(buildingLocationsDataFile, buildingsLocations, numberOfBuildings);
    }
}

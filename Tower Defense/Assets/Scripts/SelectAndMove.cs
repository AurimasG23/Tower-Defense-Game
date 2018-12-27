using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAndMove : MonoBehaviour
{
    //------------------------------------------------------------------
    private static int numberOfBuildings = 4;           //pastatu kiekis
    private static int gridDimension = 40;              //bazės pagrindo matmenys
    //------------------------------------------------------------------

    public GameObject[] buildings;                      //pastatai
    public GameObject[] buildingsBasis;                 //pastatu pagrindai (plokscia apacia)   
    public BuildingDimensions[] buildingsDimensions;    //pastatu matmenys
    public int selectedBuildingIndex = -1;

    private BuildingLocation[] buildingsLocations = new BuildingLocation[numberOfBuildings];
    private string buildingLocationsDataFile = "buildingLocations";

    private int[,] base_squares = new int[gridDimension, gridDimension];
    private string baseSquaresDataFile = "baseSquares";

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
            buildingBasisMeshRenderers[i] = buildingsBasis[i].GetComponent<MeshRenderer>();
        }

        selectedBuildingIndex = -1;

        DataFileHandler.SetBuildingsLocationsOnFirstLaunch(buildingLocationsDataFile, numberOfBuildings);
        buildingsLocations = DataFileHandler.ReadBuildingLocations(buildingLocationsDataFile, numberOfBuildings);
        for (int i = 0; i < numberOfBuildings; i++)
        {
            buildings[i].transform.position = new Vector3(buildingsLocations[i].x, buildingsLocations[i].y, buildingsLocations[i].z);
        }

        DataFileHandler.SetBuildingsSquaresOnFirstLaunch(baseSquaresDataFile, gridDimension);
        base_squares = DataFileHandler.ReadBaseSquares(baseSquaresDataFile, gridDimension);
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

    private Vector2[,] FindBuildingSquares(int biuldingIndex)
    {
        Vector2[,]  squares = new Vector2[buildingsDimensions[biuldingIndex].xLength, buildingsDimensions[biuldingIndex].zWidth];
        double startX = 0;
        double startZ = 0;
        if(buildings[biuldingIndex].transform.position.x > 0)
        {
            startX = buildings[biuldingIndex].transform.position.x - (buildingsDimensions[biuldingIndex].xLength / 2);
        }
        else
        {
            startX = buildings[biuldingIndex].transform.position.x - (buildingsDimensions[biuldingIndex].xLength / 2) - 1;
        }      
        if(buildings[biuldingIndex].transform.position.z > 0)
        {
            startZ = buildings[biuldingIndex].transform.position.z - (buildingsDimensions[biuldingIndex].zWidth / 2);
        }
        else
        {
            startZ = buildings[biuldingIndex].transform.position.z - (buildingsDimensions[biuldingIndex].zWidth / 2) - 1;
        }
        
        for (int i = 0; i < buildingsDimensions[biuldingIndex].zWidth;  i++)
        {
            for (int j = 0; j < buildingsDimensions[biuldingIndex].xLength; j++)
            {
                float x = (float)startX + j + 1;
                if(x > 0)
                {
                    squares[i, j].x = gridDimension/2 + x - 1;
                }
                else
                {
                    squares[i, j].x = gridDimension/2 + x;
                }
                float z = (float)startZ + i + 1;
                if(z > 0)
                {
                    squares[i, j].y = gridDimension/2 + z - 1;
                }
                else
                {
                    squares[i, j].y = gridDimension/2 + z;
                }
            }
        }
        return squares;
    }

    public bool EmptySpot(int buildingIndex)
    {
        Vector2[,] buildingSquares = FindBuildingSquares(buildingIndex);
        for(int i = 0; i < buildingsDimensions[buildingIndex].zWidth; i++)
        {
            for (int j = 0; j < buildingsDimensions[buildingIndex].xLength; j++)
            {
                if(base_squares[(int)buildingSquares[i,j].x, (int)buildingSquares[i, j].y] != -1)
                {
                    buildingBasisMeshRenderers[buildingIndex].material = red;
                    return false;
                }
            }
        }
        buildingBasisMeshRenderers[buildingIndex].material = green;
        return true;
    }
}

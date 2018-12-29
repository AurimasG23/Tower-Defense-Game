using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAndMove : MonoBehaviour
{
    //------------------------------------------------------------------
    private static int numberOfBuildings = 4;           //pastatų kiekis
    private static int gridDimension = 40;              //bazės pagrindo matmenys
    //------------------------------------------------------------------

    public GameObject[] buildings;                      //pastatai
    public GameObject[] buildingsBasis;                 //pastatu pagrindai (plokscia apacia)   
    public BuildingDimensions[] buildingsDimensions;    //pastatu matmenys
    public int selectedBuildingIndex = -1;              //pasirinkto pastato indeksas

    private BuildingLocation[] buildingsLocations = new BuildingLocation[numberOfBuildings];    // pastatų pozicijos
    private string buildingLocationsDataFile = "buildingLocations";                             // pastatų pozicijų duomenų failas

    private int[,] base_squares = new int[gridDimension, gridDimension];        // bazės pagrindo langeliu matrica, kur saugomi pastatų užimami plotai
    private string baseSquaresDataFile = "baseSquares";                         // bazės pagrindo langelių duomenų failas

    private MeshRenderer[] buildingBasisMeshRenderers = new MeshRenderer[numberOfBuildings];  // pastatų pagrindų mesh redereriai
    public Material red;     // raudona pastato pagrindo spalva
    public Material green;   // žalia pastato pagrindo spalva

    public GameObject selectedBuildingArrows;  // pasirinkto pastato rodyklės
    public GameObject arrow_x_p;
    public GameObject arrow_x_n;
    public GameObject arrow_z_p;
    public GameObject arrow_z_n;

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

        selectedBuildingArrows.transform.position = new Vector3(0, -100, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //-----------------------------------------------------------------------------------------------------
    public void selectBuilding(int index)
    {
        if (selectedBuildingIndex != -1)
        {           
            if(EmptySpot(selectedBuildingIndex))
            {
                ClearSquares(selectedBuildingIndex, buildingsLocations[selectedBuildingIndex]);
                buildingsLocations[selectedBuildingIndex] = new BuildingLocation(buildings[selectedBuildingIndex].transform.position.x, buildings[selectedBuildingIndex].transform.position.y, buildings[selectedBuildingIndex].transform.position.z);
                FillSquares(selectedBuildingIndex, buildingsLocations[selectedBuildingIndex]);
            }
            else
            {
                buildings[selectedBuildingIndex].transform.position = new Vector3(buildingsLocations[selectedBuildingIndex].x, buildingsLocations[selectedBuildingIndex].y, buildingsLocations[selectedBuildingIndex].z);
                buildingBasisMeshRenderers[selectedBuildingIndex].material = green;
            }          
        }
        selectedBuildingIndex = index;
        BuildingPlacement.instance.SetItem(buildings[selectedBuildingIndex], buildingsDimensions[selectedBuildingIndex]);
        SetArrowsLocalPosition(selectedBuildingIndex);
        selectedBuildingArrows.transform.position = new Vector3(buildingsLocations[selectedBuildingIndex].x, 0, buildingsLocations[selectedBuildingIndex].z);
    }

    public void DeselectBuildings()
    {
        if (selectedBuildingIndex != -1)
        {
            if (EmptySpot(selectedBuildingIndex))
            {
                ClearSquares(selectedBuildingIndex, buildingsLocations[selectedBuildingIndex]);
                buildingsLocations[selectedBuildingIndex] = new BuildingLocation(buildings[selectedBuildingIndex].transform.position.x, buildings[selectedBuildingIndex].transform.position.y, buildings[selectedBuildingIndex].transform.position.z);
                FillSquares(selectedBuildingIndex, buildingsLocations[selectedBuildingIndex]);
            }
            else
            {
                buildings[selectedBuildingIndex].transform.position = new Vector3(buildingsLocations[selectedBuildingIndex].x, buildingsLocations[selectedBuildingIndex].y, buildingsLocations[selectedBuildingIndex].z);
                buildingBasisMeshRenderers[selectedBuildingIndex].material = green;
            }
            selectedBuildingIndex = -1;
            selectedBuildingArrows.transform.position = new Vector3(0, -100, 0);
        }
    }
    //-------------------------------------------------------------------------------------------------------

    public void SaveBuildingsLocations()
    {
        //Renew();
        DataFileHandler.ChangeBuildingLocations(buildingLocationsDataFile, buildingsLocations, numberOfBuildings);
        DataFileHandler.ChangeBaseSquares(baseSquaresDataFile, base_squares, gridDimension);
    }

    private Vector2[,] FindBuildingSquares(int biuldingIndex, BuildingLocation buildingLocation)
    {
        Vector2[,]  squares = new Vector2[buildingsDimensions[biuldingIndex].xLength, buildingsDimensions[biuldingIndex].zWidth];
        double startX = 0;
        double startZ = 0;
        if(buildingLocation.x > 0)
        {
            startX = buildingLocation.x - (buildingsDimensions[biuldingIndex].xLength / 2);
        }
        else
        {
            startX = buildingLocation.x - (buildingsDimensions[biuldingIndex].xLength / 2) - 1;
        }      
        if(buildingLocation.z > 0)
        {
            startZ = buildingLocation.z - (buildingsDimensions[biuldingIndex].zWidth / 2);
        }
        else
        {
            startZ = buildingLocation.z - (buildingsDimensions[biuldingIndex].zWidth / 2) - 1;
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
        BuildingLocation location = new BuildingLocation(buildings[buildingIndex].transform.position.x, buildings[buildingIndex].transform.position.y,buildings[buildingIndex].transform.position.z);
        Vector2[,] buildingSquares = FindBuildingSquares(buildingIndex, location);
        for(int i = 0; i < buildingsDimensions[buildingIndex].zWidth; i++)
        {
            for (int j = 0; j < buildingsDimensions[buildingIndex].xLength; j++)
            {
                if(base_squares[(int)buildingSquares[i,j].x, (int)buildingSquares[i, j].y] != -1 &&
                    base_squares[(int)buildingSquares[i, j].x, (int)buildingSquares[i, j].y] != buildingIndex)
                {
                    buildingBasisMeshRenderers[buildingIndex].material = red;
                    return false;
                }
            }
        }
        buildingBasisMeshRenderers[buildingIndex].material = green;
        return true;
    }

    //langeliai uzpildomi pastato indeksu
    public void FillSquares(int buildingIndex, BuildingLocation location)
    {
        Vector2[,] buildingSquares = FindBuildingSquares(buildingIndex, location);
        for (int i = 0; i < buildingsDimensions[buildingIndex].zWidth; i++)
        {
            for (int j = 0; j < buildingsDimensions[buildingIndex].xLength; j++)
            {
                base_squares[(int)buildingSquares[i, j].x, (int)buildingSquares[i, j].y] = buildingIndex;
            }
        }
    }

    public void ClearSquares(int buildingIndex, BuildingLocation location)
    {
        Vector2[,] squaresToClear = FindBuildingSquares(buildingIndex, location);
        for (int i = 0; i < buildingsDimensions[buildingIndex].zWidth; i++)
        {
            for (int j = 0; j < buildingsDimensions[buildingIndex].xLength; j++)
            {
                if(base_squares[(int)squaresToClear[i, j].x, (int)squaresToClear[i, j].y] == buildingIndex)
                {
                    base_squares[(int)squaresToClear[i, j].x, (int)squaresToClear[i, j].y] = -1;
                }              
            }
        }
    }

    private void SetArrowsLocalPosition(int buildingIndex)
    {
        double x_value = (double)buildingsDimensions[buildingIndex].xLength / 2 + 1;
        double z_value = (double)buildingsDimensions[buildingIndex].zWidth / 2 + 1;
        arrow_x_p.transform.localPosition = new Vector3((float)x_value, 0, 0);
        arrow_x_n.transform.localPosition = new Vector3((float)-x_value, 0, 0);
        arrow_z_p.transform.localPosition = new Vector3(0, 0, (float)z_value);
        arrow_z_n.transform.localPosition = new Vector3(0, 0, (float)-z_value);
    }
    //-----------------------------------------------------------------------------------------

    private void Renew()
    {
        buildingsLocations[0] = new BuildingLocation(5.5f, 0, 5.5f);
        buildingsLocations[1] = new BuildingLocation(-5.5f, 0, 5.5f);
        buildingsLocations[2] = new BuildingLocation(-5, 0, -5);
        buildingsLocations[3] = new BuildingLocation(5, 0, -5);

        for(int i = 0; i < gridDimension; i++)
        {
            for (int j = 0; j < gridDimension; j++)
            {
                base_squares[i, j] = -1;
            }
        }

        base_squares[25, 25] = 0;
        base_squares[14, 25] = 1;
        Vector2[,] buildingSquares = FindBuildingSquares(2, buildingsLocations[2]);
        for (int i = 0; i < buildingsDimensions[2].zWidth; i++)
        {
            for (int j = 0; j < buildingsDimensions[2].xLength; j++)
            {
                base_squares[(int)buildingSquares[i, j].x, (int)buildingSquares[i, j].y] = 2;
            }
        }
        buildingSquares = FindBuildingSquares(3, buildingsLocations[3]);
        for (int i = 0; i < buildingsDimensions[3].zWidth; i++)
        {
            for (int j = 0; j < buildingsDimensions[3].xLength; j++)
            {
                base_squares[(int)buildingSquares[i, j].x, (int)buildingSquares[i, j].y] = 3;
            }
        }
    }
}

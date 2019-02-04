using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectAndMove : MonoBehaviour
{
    //------------------------------------------------------------------
    public static int numberOfBuildings = 20;           //pastatų kiekis
    public static int baseGridDimensionX_p = 30;       //langeliu kiekis x teigiamoj pusej
    public static int baseGridDimensionX_n = 40;       //langeliu kiekis x neigiamoj pusej
    public static int baseGridDimensionZ_p = 20;       //langeliu kiekis z teigiamoj pusej
    public static int baseGridDimensionZ_n = 20;       //langeliu kiekis z neigiamoj pusej
    //------------------------------------------------------------------

    public GameObject[] buildings;                      //pastatai
    public GameObject[] buildingsBasis;                 //pastatu pagrindai (plokscia apacia)   
    public BuildingDimensions[] buildingsDimensions;    //pastatu matmenys
    public int selectedBuildingIndex = -1;              //pasirinkto pastato indeksas

    private BuildingLocation[] buildingsLocations = new BuildingLocation[numberOfBuildings];    // pastatų pozicijos
    private string buildingLocationsDataFile = "buildingLocations";                             // pastatų pozicijų duomenų failas

    //private int[,] base_squares = new int[gridDimension, gridDimension];        // bazės pagrindo langeliu matrica, kur saugomi pastatų užimami plotai
    private int[,] base_squares = new int[baseGridDimensionZ_p + baseGridDimensionZ_n, baseGridDimensionX_p + baseGridDimensionX_n];        // bazės pagrindo langeliu matrica, kur saugomi pastatų užimami plotai
    private string baseSquaresDataFile = "baseSquares";                         // bazės pagrindo langelių duomenų failas

    private MeshRenderer[] buildingBasisMeshRenderers = new MeshRenderer[numberOfBuildings];  // pastatų pagrindų mesh redereriai
    public Material red;     // raudona pastato pagrindo spalva
    public Material green;   // žalia pastato pagrindo spalva

    public GameObject selectedBuildingArrows;  // pasirinkto pastato rodyklės
    public GameObject arrow_x_p;
    public GameObject arrow_x_n;
    public GameObject arrow_z_p;
    public GameObject arrow_z_n;

    public Button removeBuildingButton;


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

        DataFileHandler.SetBuildingsSquaresOnFirstLaunch(baseSquaresDataFile, baseGridDimensionX_p + baseGridDimensionX_n, baseGridDimensionZ_p + baseGridDimensionZ_n);
        base_squares = DataFileHandler.ReadBaseSquares(baseSquaresDataFile, baseGridDimensionX_p + baseGridDimensionX_n, baseGridDimensionZ_p + baseGridDimensionZ_n);

        selectedBuildingArrows.transform.position = new Vector3(0, -100, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //-----------------------------------------------------------------------------------------------------
    //Kvieciamas kai paspaudziama ant movables layer turincio pastato
    public void SelectBuilding(int index)
    {
        if (index != selectedBuildingIndex)
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

                if (selectedBuildingIndex != index)
                {
                    PutBuildingBack(selectedBuildingIndex);
                    BringBuildingNearer(index);
                }
            }
            else
            {
                BringBuildingNearer(index);
            }

            BuildingPlacement.instance.SetItem(buildings[index], buildingsDimensions[index]);

            SetArrowsLocalPosition(index);
            selectedBuildingArrows.transform.position = new Vector3(buildings[index].transform.position.x, 0, buildings[index].transform.position.z);
            BringArrowsNearer();

            selectedBuildingIndex = index;
            Debug.Log("Selected");
        }
        removeBuildingButton.gameObject.SetActive(true);
    }

    // Kvieciamas tik tada kai paspaudziama ant movables layer neturincio pastato
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
            PutBuildingBack(selectedBuildingIndex);
            selectedBuildingIndex = -1;
            selectedBuildingArrows.transform.position = new Vector3(0, -100, 0);
        }
        removeBuildingButton.gameObject.SetActive(false);
    }
    //-------------------------------------------------------------------------------------------------------

    public void SaveBuildingsLocations()
    {
        //Renew();
        DataFileHandler.ChangeBuildingLocations(buildingLocationsDataFile, buildingsLocations, numberOfBuildings);
        DataFileHandler.ChangeBaseSquares(baseSquaresDataFile, base_squares, baseGridDimensionX_p + baseGridDimensionX_n, baseGridDimensionZ_p + baseGridDimensionZ_n);
        AddAndRemove.instance.SaveMoneyValue();
    }

    private Vector2[,] FindBuildingSquares(int biuldingIndex, BuildingLocation buildingLocation)
    {
        Vector2[,]  squares = new Vector2[buildingsDimensions[biuldingIndex].xLength, buildingsDimensions[biuldingIndex].zWidth];
        double startX = 0;
        double startZ = 0;
        if(buildingLocation.x > 0)
        {
            startX = (double)buildingLocation.x - ((double)buildingsDimensions[biuldingIndex].xLength / 2);            
        }
        else
        {
            startX = (double)buildingLocation.x - ((double)buildingsDimensions[biuldingIndex].xLength / 2);
        }      
        if(buildingLocation.z > 0)
        {
            startZ = (double)buildingLocation.z - ((double)buildingsDimensions[biuldingIndex].zWidth / 2);
        }
        else
        {
            startZ = (double)buildingLocation.z - ((double)buildingsDimensions[biuldingIndex].zWidth / 2);
        }

        for (int i = 0; i < buildingsDimensions[biuldingIndex].zWidth;  i++)
        {
            for (int j = 0; j < buildingsDimensions[biuldingIndex].xLength; j++)
            {              
                squares[i, j].x = baseGridDimensionX_n + (float)startX + j;
                squares[i, j].y = baseGridDimensionZ_n + (float)startZ + i;                         
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
                if(base_squares[(int)buildingSquares[i,j].y, (int)buildingSquares[i, j].x] != -1 &&
                    base_squares[(int)buildingSquares[i, j].y, (int)buildingSquares[i, j].x] != buildingIndex)
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
                base_squares[(int)buildingSquares[i, j].y, (int)buildingSquares[i, j].x] = buildingIndex;
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
                if(base_squares[(int)squaresToClear[i, j].y, (int)squaresToClear[i, j].x] == buildingIndex)
                {
                    base_squares[(int)squaresToClear[i, j].y, (int)squaresToClear[i, j].x] = -1;
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

    private void BringBuildingNearer(int buildingIndex)
    {
        CanonManager canonManager = buildings[buildingIndex].GetComponent(typeof(CanonManager)) as CanonManager;
        CrossbowManager crossbowManager = buildings[buildingIndex].GetComponent(typeof(CrossbowManager)) as CrossbowManager;
        if (canonManager != null)
        {            
            canonManager.BringNearer();
        }
        else if(crossbowManager != null)
        {
            crossbowManager.BringNearer();
        }
    }

    private void PutBuildingBack(int buildingIndex)
    {
        CanonManager canonManager = buildings[buildingIndex].GetComponent(typeof(CanonManager)) as CanonManager;
        CrossbowManager crossbowManager = buildings[buildingIndex].GetComponent(typeof(CrossbowManager)) as CrossbowManager;
        if (canonManager != null)
        {
            canonManager.PutBack();
        }
        else if (crossbowManager != null)
        {
            crossbowManager.PutBack();
        }
    }

    public void BringArrowsNearer()
    {
        Vector3 CameraDirection = Camera.main.transform.forward * (-10);
        arrow_x_p.transform.position = new Vector3(arrow_x_p.transform.position.x + CameraDirection.x, arrow_x_p.transform.position.y + CameraDirection.y, arrow_x_p.transform.position.z + CameraDirection.z);
        arrow_x_n.transform.position = new Vector3(arrow_x_n.transform.position.x + CameraDirection.x, arrow_x_n.transform.position.y + CameraDirection.y, arrow_x_n.transform.position.z + CameraDirection.z);
        arrow_z_p.transform.position = new Vector3(arrow_z_p.transform.position.x + CameraDirection.x, arrow_z_p.transform.position.y + CameraDirection.y, arrow_z_p.transform.position.z + CameraDirection.z);
        arrow_z_n.transform.position = new Vector3(arrow_z_n.transform.position.x + CameraDirection.x, arrow_z_n.transform.position.y + CameraDirection.y, arrow_z_n.transform.position.z + CameraDirection.z);
    }
    //-----------------------------------------------------------------------------------------

    public int GetIndexOfAvailableBuilding(int startIndex, int bound)
    {
        for(int i = startIndex; i < bound; i++)
        {
            if(buildingsLocations[i].y == -100)
            {
                return i;
            }
        }
        return -1;
    }

    public void AddBuilding(int startIndex, int bound)
    {
        int index = GetIndexOfAvailableBuilding(startIndex, bound);
        float x, z;
        if(buildingsDimensions[index].xLength % 2 != 0)
        {
            x = 0.5f;
        }
        else
        {
            x = 0;
        }
        if (buildingsDimensions[index].zWidth % 2 != 0)
        {
            z = 0.5f;
        }
        else
        {
            z = 0;
        }
        buildings[index].transform.position = new Vector3(x, 0, z);

        buildingBasisMeshRenderers[index].material = red;

        if (index != -1)
        {
            SelectBuilding(index);
        }
    }

    public void RemoveBuilding()
    {
        buildings[selectedBuildingIndex].transform.position = new Vector3(0, -100, 0);
        BuildingLocation location = buildingsLocations[selectedBuildingIndex];
        ClearSquares(selectedBuildingIndex, location);
        selectedBuildingIndex = -1;
        selectedBuildingArrows.transform.position = new Vector3(0, -100, 0);
        Debug.Log("RemoveBuilding");
    }

    //-----------------------------------------------------------------------------------------

    //private void Renew()
    //{
    //    buildingsLocations[0] = new BuildingLocation(10.5f, 0, 10.5f);
    //    buildingsLocations[1] = new BuildingLocation(-10.5f, 0, 10.5f);
    //    buildingsLocations[2] = new BuildingLocation(-10.5f, 0, -10.5f);
    //    buildingsLocations[3] = new BuildingLocation(10.5f, 0, -10.5f);

    //    for (int i = 0; i < baseGridDimensionZ_p + baseGridDimensionZ_n; i++)
    //    {
    //        for (int j = 0; j < baseGridDimensionX_p + baseGridDimensionX_n; j++)
    //        {
    //            if (i >= 14 && i <= 25)
    //            {
    //                base_squares[i, j] = -2; // kelias ir sienos
    //            }
    //            else
    //            {
    //                base_squares[i, j] = -1;
    //            }
    //        }
    //    }

    //    FillSquares(0, buildingsLocations[0]);
    //    FillSquares(1, buildingsLocations[1]);
    //    FillSquares(2, buildingsLocations[2]);      
    //    FillSquares(3, buildingsLocations[3]);
    //}

    private void Renew()
    {
        for(int i = 0; i < numberOfBuildings; i++)
        {
            buildingsLocations[i] = new BuildingLocation(0, -100, 0);
        }       

        for (int i = 0; i < baseGridDimensionZ_p + baseGridDimensionZ_n; i++)
        {
            for (int j = 0; j < baseGridDimensionX_p + baseGridDimensionX_n; j++)
            {
                if (i >= 14 && i <= 25)
                {
                    base_squares[i, j] = -2; // kelias ir sienos
                }
                else
                {
                    base_squares[i, j] = -1;
                }
            }
        }

        PlayerPrefs.SetInt("money", 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public static int numberOfBuildings = 20;           //pastatų kiekis

    public GameObject[] buildings;                      //pastatai

    private BuildingLocation[] buildingsLocations = new BuildingLocation[numberOfBuildings];    // pastatų pozicijos
    private string buildingLocationsDataFile = "buildingLocations";                             // pastatų pozicijų duomenų failas

    // Use this for initialization
    void Start ()
    {
        DataFileHandler.SetBuildingsLocationsOnFirstLaunch(buildingLocationsDataFile, numberOfBuildings);
        buildingsLocations = DataFileHandler.ReadBuildingLocations(buildingLocationsDataFile, numberOfBuildings);
        for (int i = 0; i < numberOfBuildings; i++)
        {
            buildings[i].transform.position = new Vector3(buildingsLocations[i].x, buildingsLocations[i].y, buildingsLocations[i].z);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

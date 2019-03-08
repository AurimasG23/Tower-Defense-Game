using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public GameObject canonContainer;
    public GameObject crossbowContainer;
    public Transform[] defences;
    private int canonCount;
    private int crossbowCount;

    private BuildingLocation[] buildingsLocations;                                              // pastatų pozicijos
    private string buildingLocationsDataFile = "buildingLocations";                             // pastatų pozicijų duomenų failas

    // Use this for initialization
    void Start ()
    {
        canonCount = canonContainer.transform.childCount;
        crossbowCount = crossbowContainer.transform.childCount;
        defences = new Transform[canonCount + crossbowCount];
        buildingsLocations = new BuildingLocation[canonCount + crossbowCount];
        for (int i = 0; i < canonCount + crossbowCount; i++)
        {
            if(i < canonCount)
            {
                defences[i] = canonContainer.transform.GetChild(i);
            }
            else
            {
                defences[i] = crossbowContainer.transform.GetChild(i - canonCount);
            }
        }

        DataFileHandler.SetBuildingsLocationsOnFirstLaunch(buildingLocationsDataFile, canonCount + crossbowCount);        
        buildingsLocations = DataFileHandler.ReadBuildingLocations(buildingLocationsDataFile, canonCount + crossbowCount);        
        for (int i = 0; i < canonCount + crossbowCount; i++)
        {
            defences[i].transform.position = new Vector3(buildingsLocations[i].x, buildingsLocations[i].y, buildingsLocations[i].z);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }  
}

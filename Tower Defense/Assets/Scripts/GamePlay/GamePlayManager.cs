using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    public GameObject canonContainer;
    public GameObject crossbowContainer;
    public Transform[] defences;
    private int canonCount;
    private int crossbowCount;

    private BuildingLocation[] buildingsLocations;                                              // pastatų pozicijos
    private string buildingLocationsDataFile = "buildingLocations";                             // pastatų pozicijų duomenų failas

    private int liveCount = 5;
    public Text livesText;

    public static GamePlayManager instance;

    // Use this for initialization
    void Start ()
    {
        instance = this;

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

        livesText.text = "Lives: " + liveCount.ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }  

    public void DecreaseLiveCount()
    {
        if (liveCount > 0)
        {
            liveCount--;
            livesText.text = "Lives: " + liveCount.ToString();
        }
    }
}

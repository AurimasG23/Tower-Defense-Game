using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GamePlayManager : MonoBehaviour
{
    public GameObject canonContainer;
    public GameObject crossbowContainer;
    public Transform[] defences;
    private int canonCount;
    private int crossbowCount;

    private BuildingLocation[] buildingsLocations;                                              // pastatų pozicijos
    private string buildingLocationsDataFile = "buildingLocations";                             // pastatų pozicijų duomenų failas

    private string enemyTag = "Enemy";

    private int liveCount = 5;
    public Text livesText;

    private int score;
    public Text scoreText;
    private float scoreTimeInterval = 1f;
    private float timer;

    public GameObject endGamePanel;
    public Text totalScoreText;
    public InputField nameInputField;

    private int selectedObstacleIndex;
    [SerializeField]
    private LayerMask movelessLayer;
    private float clickTime;
    private float maxObstacleClickDuration = 0.3f;
    public GameObject bombPrefab;
    public Transform bombSpawnPoint;   //kulkos atsiradimo vieta
    private Vector3 targetPosition;

    private bool isGamePaused;

    public static GamePlayManager instance;

    // Use this for initialization
    void Start ()
    {
        instance = this;

        isGamePaused = false;

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

        timer = 0;

        selectedObstacleIndex = -1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if(timer >= scoreTimeInterval)
        {
            if(!IsGamePaused())
            {
                IncreaseScore(1);
            }
            timer = 0;
        }

        if(selectedObstacleIndex == 0)
        {
            //if(!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetKeyDown(KeyBindManager.MyInstance.Keybinds["Button(Click)"]))
            {
                RaycastHit rayHit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity,
                    movelessLayer, QueryTriggerInteraction.UseGlobal))
                {
                    Debug.Log("ground");
                    targetPosition = new Vector3(rayHit.point.x, 0, rayHit.point.z); ;
                    clickTime = Time.timeSinceLevelLoad;
                }
            }
        }

        //if (Input.GetMouseButtonUp(0))
        if (Input.GetKeyUp(KeyBindManager.MyInstance.Keybinds["Button(Click)"]))
        {
            if (Time.timeSinceLevelLoad - clickTime <= maxObstacleClickDuration)
            {              
                DeselectObstacle();
                ThrowBomb();
            }
        }
    }  

    public void DecreaseLiveCount()
    {
        if (liveCount > 0)
        {
            liveCount--;
            livesText.text = "Lives: " + liveCount.ToString();
        }
        if(liveCount <= 0)
        {
            EndGame();
        }
    }

    public void IncreaseScore(int value)
    {
        if (liveCount > 0)
        {
            score += value;
            scoreText.text = "Score: " + score.ToString();
        }
    }

    public void EndGame()
    {
        totalScoreText.text = "Total score: " + score.ToString();
        endGamePanel.transform.gameObject.SetActive(true);
        DestroyAllEnemies();
    }

    public int GetLiveCount()
    {
        return liveCount;
    }

    public void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyHealth>().Die();
        }
    }

    public void ConfirmScore()
    {
        string name = nameInputField.text;
        if (name == "")
        {
            name = "Player";
        }
        LeaderboardManager.instance.SubmitNickname(name, score);
        SceneManager.LoadScene("Menu");
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }

    public void PauseGame()
    {
        isGamePaused = true;
    }

    public void UnpauseGame()
    {
        isGamePaused = false;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SelectObstacle(int index)
    {       
        selectedObstacleIndex = index;
    }

    public void DeselectObstacle()
    {
        selectedObstacleIndex = -1;
    }

    public void ThrowBomb()
    {
        GameObject newBomb = (GameObject)Instantiate(bombPrefab, bombSpawnPoint.position, bombSpawnPoint.rotation);
        Bomb bomb = newBomb.GetComponent<Bomb>();

        if (bomb != null)
        {
            bomb.Seek(targetPosition);
        }
    }
}

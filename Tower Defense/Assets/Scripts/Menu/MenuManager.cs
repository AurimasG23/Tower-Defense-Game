using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    static int leaderboardSize = 10;
    public GameObject leaderNamesContainer;
    public Text[] leaderNamesTexts;
    public GameObject leaderScoresContainer;
    public Text[] leaderScoresTexts = new Text[leaderboardSize];

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < leaderboardSize; i++)
        {
            //leaderNamesTexts[i] = leaderNamesContainer.GetComponentInChildren<Text>;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoToBaseEditor()
    {
        SceneManager.LoadScene("BaseEditor");
    }

    public void Play()
    {
        SceneManager.LoadScene("GamePlay");
    }
}

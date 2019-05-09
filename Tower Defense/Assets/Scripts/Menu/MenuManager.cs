using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    static int leaderboardSize = 10;
    public Text[] leaderNamesTexts = new Text[leaderboardSize];
    public Text[] leaderScoresTexts = new Text[leaderboardSize];
    int countOfLeaders = 0;

    string dataFile = "leaderboard";

    // Use this for initialization
    void Start ()
    {
        //PlayerPrefs.SetInt("countOfLeaders", 0); //clear
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

    public void ShowScores()
    {
        if (PlayerPrefs.HasKey("countOfLeaders"))
        {
            countOfLeaders = PlayerPrefs.GetInt("countOfLeaders");

            string[] names = new string[leaderboardSize];
            int[] scores = new int[leaderboardSize];
            DataFileHandler.ReadHighScores(dataFile, names, scores);

            for (int i = 0; i < leaderboardSize; i++)
            {
                if(i < countOfLeaders)
                {
                    string numeration;
                    if (i+1 < 10)
                    {
                        numeration = "  " + (i + 1).ToString() + ".  ";
                    }
                    else
                    {
                        numeration = (i + 1).ToString() + ".  ";
                    }
                    leaderNamesTexts[i].text =  numeration + names[i].ToString();
                    leaderScoresTexts[i].text = scores[i].ToString();
                }
                else
                {
                    leaderNamesTexts[i].text = "";
                    leaderScoresTexts[i].text = "";
                }
            }
        }
        else
        {
            countOfLeaders = 0;
            for (int i = 0; i < leaderboardSize; i++)
            {
                leaderNamesTexts[i].text = "";
                leaderScoresTexts[i].text = "";
            }
        }
    }
}

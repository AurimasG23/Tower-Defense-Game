using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    static int leaderboardSize = 10;
    string[] leaderNames = new string[leaderboardSize];
    int[] leaderScores = new int[leaderboardSize];

    string dataFile = "leaderboard";

    int countOfLeaders = 0;

    public static LeaderboardManager instance;

    // Use this for initialization
    void Start()
    {
        instance = this;

        if (PlayerPrefs.HasKey("countOfLeaders"))
        {
            countOfLeaders = PlayerPrefs.GetInt("countOfLeaders");
            DataFileHandler.ReadHighScores(dataFile, countOfLeaders, out leaderNames, out leaderScores);
        }
        else
        {
            countOfLeaders = 0;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SubmitNickname(string name, int score)
    {      
        InsertIntoLeaders(name, score);
        if (countOfLeaders < leaderboardSize)
        {
            countOfLeaders++;
        }
        DataFileHandler.SaveHighScores(dataFile, countOfLeaders, leaderNames, leaderScores);
        PlayerPrefs.SetInt("countOfLeaders", countOfLeaders);
    }

    private void InsertIntoLeaders(string name, int score)
    {
        int index = findIndexForInsertion(score);
        if (index < 10)
        {
            if (countOfLeaders == 0 || index == countOfLeaders)
            {
                leaderNames[countOfLeaders] = name;
                leaderScores[countOfLeaders] = score;
            }
            else
            {
                for (int i = countOfLeaders - 1; i > index; i--)
                {
                    leaderScores[i] = leaderScores[i - 1];
                    leaderNames[i] = leaderNames[i - 1];
                }
                leaderScores[index] = score;
                leaderNames[index] = name;
            }
        }
    }

    private int findIndexForInsertion(int value)
    {
        if (countOfLeaders == 0)
        {
            return 0;
        }
        for (int i = 0; i < countOfLeaders; i++)
        {
            if (value > leaderScores[i])
            {
                return i;
            }
            if (countOfLeaders < 10 && i == countOfLeaders - 1)
            {
                return i + 1;
            }
        }
        return countOfLeaders;
    }
}

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

    public Slider menuMusicSlider;
    public AudioSource menuMusicSource;
    public Slider sfxSlider;
    public AudioClip menuButtonSound;

    // Use this for initialization
    void Start ()
    {
        //PlayerPrefs.SetInt("countOfLeaders", 0); //clear

        if (PlayerPrefs.HasKey("menuVolume"))
        {
            menuMusicSlider.value = PlayerPrefs.GetFloat("menuVolume");
            menuMusicSource.volume = PlayerPrefs.GetFloat("menuVolume");
        }

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("sfxVolume", 0.3f);
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
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

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayButtonSound()
    {
        AudioManager.instance.Play(menuButtonSound, this.gameObject);
    }

    public void ChangeMenuVolume()
    {
        menuMusicSource.volume = menuMusicSlider.value;
        PlayerPrefs.SetFloat("menuVolume", menuMusicSlider.value);
    }

    public void ChangeSFXVolume()
    {
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }
}

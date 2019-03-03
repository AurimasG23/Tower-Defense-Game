using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    AudioSource[] bgMusicSources = new AudioSource[5];
    public GameObject[] bgMusicObjects = new GameObject[5];
    bool[] bgMusicPlaying = new bool[5];


    public GameObject[] objectsWithAudioSOurces = new GameObject[10];
    AudioSource[] soundsSources = new AudioSource[10];

    public GameObject musicPanel;
    bool musicPanelOpened = false;

	// Use this for initialization
	void Start ()
    {
        musicPanel.transform.localPosition = new Vector2(0, 312);
        musicPanelOpened = false;

        for(int i = 0; i < bgMusicSources.Length; i++)
        {
            bgMusicSources[i] = bgMusicObjects[i].GetComponent<AudioSource>();
            bgMusicPlaying[i] = false;
        }

        bgMusicSources[0].Play();
        bgMusicPlaying[0] = true;


        for (int i = 0; i < objectsWithAudioSOurces.Length; i++)
        {
            soundsSources[i] = objectsWithAudioSOurces[i].GetComponent<AudioSource>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        for(int i = 0; i < bgMusicSources.Length; i++)
        {
            if(bgMusicPlaying[i] == true && !bgMusicSources[i].isPlaying)
            {
                bgMusicSources[i].Play();
            }
        }       
    }

    public void SetBackgroundMusic(int index)
    {
        if (!bgMusicPlaying[index])
        {
            bgMusicSources[index].Play();
            bgMusicPlaying[index] = true;
        }
        else
        {
            bgMusicSources[index].Stop();
            bgMusicPlaying[index] = false;
        }
    }

    public void StopAllBgMusic()
    {
        for (int i = 0; i < bgMusicSources.Length; i++)
        {
            bgMusicSources[i].Stop();
            bgMusicPlaying[i] = false;
        }
    }

    public void PlayAllBgMusic()
    {
        for (int i = 0; i < bgMusicSources.Length; i++)
        {
            bgMusicSources[i].Play();
            bgMusicPlaying[i] = true;
        }
    }

    public void PlaySound(int index)
    {
        soundsSources[index].Play();
    }

    public void PlayAllSounds()
    {
        for (int i = 0; i < objectsWithAudioSOurces.Length; i++)
        {
            soundsSources[i].Play();
        }
    }

    public void StopAllSounds()
    {
        for (int i = 0; i < objectsWithAudioSOurces.Length; i++)
        {
            soundsSources[i].Stop();
        }
    }

    public void ManageMusicPanel()
    {
        if(!musicPanelOpened)
        {
            musicPanel.transform.localPosition = new Vector2(0, 135);
            musicPanelOpened = true;
        }
        else
        {
            musicPanel.transform.localPosition = new Vector2(0, 312);
            musicPanelOpened = false;
        }
    }
}

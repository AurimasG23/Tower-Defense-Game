using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    private float volume = 1;

    private void Start()
    {
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            volume = PlayerPrefs.GetFloat("sfxVolume");
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            volume = PlayerPrefs.GetFloat("sfxVolume");
        }
    }

    public int Play(AudioClip clip, GameObject obj, float pitch = 1, bool loop = false, float volumeLevel = 1)
    {
        AudioSource[] sources = obj.GetComponents<AudioSource>();
        int i = 0;
        foreach (AudioSource aud in sources)
        {
            if (!aud.isPlaying)
            {
                break;
            }
            i++;
        }

        AudioSource source = null;

        if (i == sources.Length)
        {
            source = obj.AddComponent<AudioSource>();
        }
        else
        {
            source = sources[i];
        }

        //source = obj.AddComponent<AudioSource>();
        source.pitch = pitch;
        source.loop = loop;
        source.volume = volume * volumeLevel;
        source.clip = clip;
        source.playOnAwake = false;
        source.Play();

        return i;
    }

    public void Stop(int i, GameObject obj)
    {
        if (obj.GetComponents<AudioSource>().Length > i)
            obj.GetComponents<AudioSource>()[i].Stop();
    }

    public bool isPlaying(int i, GameObject obj)
    {
        if (obj.GetComponents<AudioSource>().Length > i)
            return obj.GetComponents<AudioSource>()[i].isPlaying;
        else return false;
    }

    public void FadeOut(int i, GameObject obj, float duration)
    {
        StartCoroutine(Fade(obj.GetComponents<AudioSource>()[i], duration));
    }

    IEnumerator Fade(AudioSource source, float duration)
    {
        float vol = source.volume / duration;
        while (source.volume > 0)
        {
            source.volume -= vol * Time.deltaTime;
            yield return null;
        }
        source.Stop();
    }

    public void Pause(int i, GameObject obj)
    {
        if (obj.GetComponents<AudioSource>().Length > i)
            obj.GetComponents<AudioSource>()[i].Pause();
    }

    public void UnPause(int i, GameObject obj)
    {
        if (obj.GetComponents<AudioSource>().Length > i)
        {
            obj.GetComponents<AudioSource>()[i].UnPause();
        }
    }

    public void UnPause(int i, GameObject obj, float volumeLevel = 1)
    {
        if (obj.GetComponents<AudioSource>().Length > i)
        {
            AudioSource audio = obj.GetComponents<AudioSource>()[i];
            audio.volume = volume * volumeLevel;
            audio.UnPause();
        }
    }

    public void SetTime(int i, GameObject obj, float time)
    {
        if (obj.GetComponents<AudioSource>().Length > i)
            obj.GetComponents<AudioSource>()[i].time = time;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class KeyBindManager : MonoBehaviour
{
    private int buttonIndex = -1;

    private static KeyBindManager instance;

    public static KeyBindManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<KeyBindManager>();
            }
            return instance;
        }
    }

    public Dictionary<string, KeyCode> Keybinds { get; private set; }

    private string bindName;

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(gameObject);
        Keybinds = new Dictionary<string, KeyCode>();

        BindKey("Button(Click)", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Button(Click)", "Mouse0")));
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (bindName != string.Empty)
        {
            if (Input.GetMouseButtonDown(3))
            {
                buttonIndex = 3;
            }
            if (Input.GetMouseButtonDown(4))
            {
                buttonIndex = 4;
            }
            if (Input.GetMouseButtonDown(5))
            {
                buttonIndex = 5;
            }
            if (Input.GetMouseButtonDown(6))
            {
                buttonIndex = 6;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                buttonIndex = 7;
            }
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                buttonIndex = 8;
            }
        }
    }

    void BindKey(string key, KeyCode keyBind)
    {
        Dictionary<string, KeyCode> currentDictionary = Keybinds;

        if (!currentDictionary.ContainsKey(key))
        {
            currentDictionary.Add(key, keyBind);
            if (SceneManager.GetActiveScene().name == "Menu")
            {
                MenuManager.instance.UpdateKeyText(key, keyBind);
            }
        }
        else if (currentDictionary.ContainsValue(keyBind))
        {
            string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;
            currentDictionary[myKey] = KeyCode.None;
            MenuManager.instance.UpdateKeyText(myKey, KeyCode.None);
        }

        currentDictionary[key] = keyBind;
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            MenuManager.instance.UpdateKeyText(key, keyBind);
        }
        bindName = string.Empty;
    }

    public void KeyBindOnClick(string bindName)
    {
        this.bindName = bindName;
    }

    private void OnGUI()
    {
        if (bindName != string.Empty)
        {
            Event e = Event.current;

            if (e.isKey)
            {
                BindKey(bindName, e.keyCode);
                SaveKeys();
            }
            if (e.isMouse)
            {
                if (e.button == 0)
                {
                    BindKey(bindName, (KeyCode)System.Enum.Parse(typeof(KeyCode), "Mouse0"));
                }
                if (e.button == 1)
                {
                    BindKey(bindName, (KeyCode)System.Enum.Parse(typeof(KeyCode), "Mouse1"));
                }
                if (e.button == 2)
                {
                    BindKey(bindName, (KeyCode)System.Enum.Parse(typeof(KeyCode), "Mouse2"));
                }
                SaveKeys();
            }
            if (buttonIndex != -1)
            {
                if (buttonIndex == 3)
                {
                    BindKey(bindName, (KeyCode)System.Enum.Parse(typeof(KeyCode), "Mouse3"));
                    buttonIndex = -1;
                }
                else if (buttonIndex == 4)
                {
                    BindKey(bindName, (KeyCode)System.Enum.Parse(typeof(KeyCode), "Mouse4"));
                    buttonIndex = -1;
                }
                else if (buttonIndex == 5)
                {
                    BindKey(bindName, (KeyCode)System.Enum.Parse(typeof(KeyCode), "Mouse5"));
                    buttonIndex = -1;
                }
                else if (buttonIndex == 6)
                {
                    BindKey(bindName, (KeyCode)System.Enum.Parse(typeof(KeyCode), "Mouse6"));
                    buttonIndex = -1;
                }
                else if (buttonIndex == 7)
                {
                    BindKey(bindName, (KeyCode)System.Enum.Parse(typeof(KeyCode), "LeftShift"));
                    buttonIndex = -1;
                }
                else if (buttonIndex == 8)
                {
                    BindKey(bindName, (KeyCode)System.Enum.Parse(typeof(KeyCode), "RightShift"));
                    buttonIndex = -1;
                }
                SaveKeys();
            }
        }
    }

    public void SaveKeys()
    {
        foreach (var key in Keybinds)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }
}

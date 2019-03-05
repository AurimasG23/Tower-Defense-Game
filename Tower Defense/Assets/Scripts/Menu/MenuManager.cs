using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
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

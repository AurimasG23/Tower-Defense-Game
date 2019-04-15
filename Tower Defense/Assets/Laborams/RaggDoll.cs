using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaggDoll : MonoBehaviour {


    public GameObject character;
    public GameObject characterRagDoll;

    // Use this for initialization
    void Start ()
    {
        addRagdoll();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void addRagdoll()
    {
        Instantiate(characterRagDoll, character.transform.position, Quaternion.identity);
        character.SetActive(false);
    }
}

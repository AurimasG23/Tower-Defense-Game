using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDoorForceManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenDoors()
    {
        transform.GetComponent<Rigidbody>().AddForce(new Vector3(
            transform.rotation.x * 1000, transform.rotation.y,
            transform.rotation.z), ForceMode.Acceleration);   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "character")
        {
            OpenDoors();
        }
    }
}

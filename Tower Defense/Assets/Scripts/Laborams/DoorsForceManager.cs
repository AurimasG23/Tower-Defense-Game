using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsForceManager : MonoBehaviour
{
    public GameObject doorsBlue;
    public GameObject doorsRed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenDoors()
    {
        doorsBlue.transform.GetComponent<Rigidbody>().AddForce(new Vector3(
            doorsBlue.transform.rotation.x, doorsBlue.transform.rotation.y * 1000, 
            doorsBlue.transform.rotation.z), ForceMode.Acceleration);
        doorsRed.transform.GetComponent<Rigidbody>().AddForce(new Vector3(
            doorsRed.transform.rotation.x, doorsRed.transform.rotation.y * 1000, 
            doorsRed.transform.rotation.z), ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "character")
        {
            OpenDoors();
        }
    }
}

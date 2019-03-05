using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonManager : MonoBehaviour
{
    public GameObject canon;      //visas objektas

    public GameObject canon_parts;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BringNearer()
    {
        Debug.Log("BringNearer");
        Vector3 CameraDirection = Camera.main.transform.forward * (-10);
        Debug.Log("Camera x   " + CameraDirection.x.ToString());
        canon_parts.transform.position = new Vector3(canon_parts.transform.position.x + CameraDirection.x, canon_parts.transform.position.y + CameraDirection.y, canon_parts.transform.position.z + CameraDirection.z);
    }

    public void PutBack()
    {       
        canon_parts.transform.localPosition = new Vector3(0, 0, 0);
    }
}

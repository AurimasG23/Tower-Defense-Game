using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowManager : MonoBehaviour
{
    public GameObject crossbowTower;  //visas objektas

    public GameObject crossbowTower_parts;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BringNearer()
    {
        Vector3 CameraDirection = Camera.main.transform.forward * (-10);
        crossbowTower_parts.transform.position = new Vector3(crossbowTower_parts.transform.position.x + CameraDirection.x, crossbowTower_parts.transform.position.y + CameraDirection.y, crossbowTower_parts.transform.position.z + CameraDirection.z);
    }

    public void PutBack()
    {
        crossbowTower_parts.transform.localPosition = new Vector3(0, 0, 0);
    }
}

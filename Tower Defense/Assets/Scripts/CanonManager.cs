using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonManager : MonoBehaviour
{
    public GameObject canon;      //visas objektas

    public GameObject zole;
    public GameObject remas;
    public GameObject platforma;
    public GameObject begiai;
    public GameObject laikikliai;
    public GameObject vamzdis;
    public GameObject vamzdzio_laikiklis;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BringNearer()
    {
        Vector3 CameraDirection = Camera.main.transform.forward * (-5);
        zole.transform.position = new Vector3(zole.transform.position.x + CameraDirection.x, zole.transform.position.y + CameraDirection.y, zole.transform.position.z + CameraDirection.z);
        remas.transform.position = new Vector3(remas.transform.position.x + CameraDirection.x, remas.transform.position.y + CameraDirection.y, remas.transform.position.z + CameraDirection.z);
        platforma.transform.position = new Vector3(platforma.transform.position.x + CameraDirection.x, platforma.transform.position.y + CameraDirection.y, platforma.transform.position.z + CameraDirection.z);
        begiai.transform.position = new Vector3(begiai.transform.position.x + CameraDirection.x, begiai.transform.position.y + CameraDirection.y, begiai.transform.position.z + CameraDirection.z);
        laikikliai.transform.position = new Vector3(laikikliai.transform.position.x + CameraDirection.x, laikikliai.transform.position.y + CameraDirection.y, laikikliai.transform.position.z + CameraDirection.z);
        vamzdis.transform.position = new Vector3(vamzdis.transform.position.x + CameraDirection.x, vamzdis.transform.position.y + CameraDirection.y, vamzdis.transform.position.z + CameraDirection.z);
        vamzdzio_laikiklis.transform.position = new Vector3(vamzdzio_laikiklis.transform.position.x + CameraDirection.x, vamzdzio_laikiklis.transform.position.y + CameraDirection.y, vamzdzio_laikiklis.transform.position.z + CameraDirection.z);
        //BoxCollider boxCollider = canon.GetComponent(typeof(BoxCollider)) as BoxCollider;
        //boxCollider.center = new Vector3(boxCollider.center.x + CameraDirection.x, boxCollider.center.y + CameraDirection.y, boxCollider.center.z + CameraDirection.z);
    }
}

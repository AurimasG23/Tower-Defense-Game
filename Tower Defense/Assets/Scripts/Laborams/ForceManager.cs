using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SphereForce()
    {
        transform.GetComponent<Rigidbody>().AddForce(new Vector3(transform.position.x, transform.position.y * 300, transform.position.z), ForceMode.Acceleration);
        transform.GetComponent<Rigidbody>().useGravity = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "character")
        {
            SphereForce();
        }     
    }
}

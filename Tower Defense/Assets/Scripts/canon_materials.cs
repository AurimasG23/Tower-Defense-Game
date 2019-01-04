using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canon_materials : MonoBehaviour
{
    public GameObject zole;
    public GameObject platforma;
    public GameObject laikikliai;
    public GameObject vamzdzio_laikiklis;
    public GameObject vamzdis;
    public GameObject begiai;
    public GameObject remas;

    public Material zole_standard;
    public Material platforma_standard;
    public Material laikikliai_standard;
    public Material vamzdzio_laikiklis_standard;
    public Material vamzdis_standard;
    public Material begiai_standard;
    public Material remas_standard;

    public Material zole_priority;
    public Material platforma_priority;
    public Material laikikliai_priority;
    public Material vamzdzio_laikiklis_priority;
    public Material vamzdis_priority;
    public Material begiai_priority;
    public Material remas_priority;



    public static canon_materials instance;

    // Use this for initialization
    void Start ()
    {
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

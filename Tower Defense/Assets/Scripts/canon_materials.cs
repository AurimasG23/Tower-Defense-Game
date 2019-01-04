using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon_materials : MonoBehaviour
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
    public Material platforma_sonai_standard;
    public Material laikikliai_standard;
    public Material vamzdzio_laikiklis_standard;
    public Material vamzdis_standard;
    public Material begiai_standard;
    public Material remas_standard;

    public Material zole_priority;
    public Material platforma_priority;
    public Material platforma_sonai_priority;
    public Material laikikliai_priority;
    public Material vamzdzio_laikiklis_priority;
    public Material vamzdis_priority;
    public Material begiai_priority;
    public Material remas_priority;

    private MeshRenderer zole_MR;
    private MeshRenderer platforma_MR;
    private MeshRenderer laikikliai_MR;
    private MeshRenderer vamzdzio_laikiklis_MR;
    private MeshRenderer vamzdis_MR;
    private MeshRenderer begiai_MR;
    private MeshRenderer remas_MR;


    // Use this for initialization
    void Start ()
    {
        zole_MR = zole.GetComponent<MeshRenderer>();
        platforma_MR = platforma.GetComponent<MeshRenderer>();
        laikikliai_MR = laikikliai.GetComponent<MeshRenderer>();
        vamzdzio_laikiklis_MR = vamzdzio_laikiklis.GetComponent<MeshRenderer>();
        vamzdis_MR = vamzdis.GetComponent<MeshRenderer>();
        begiai_MR = begiai.GetComponent<MeshRenderer>();
        remas_MR = remas.GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //when selected
    public void ShowOnTop()
    {
        zole_MR.material = zole_priority;
        platforma_MR.materials[0] = platforma_sonai_priority;
        platforma_MR.materials[1] = platforma_priority;
        laikikliai_MR.material = laikikliai_priority;
        vamzdzio_laikiklis_MR.material = vamzdzio_laikiklis_priority;
        vamzdis_MR.material = vamzdis_priority;
        begiai_MR.material = begiai_priority;
        remas_MR.material = remas_priority;
    }

    public void ShowNormally()
    {
        zole_MR.material = zole_standard;
        remas_MR.material = remas_standard;
        platforma_MR.materials[0] = platforma_sonai_standard;
        platforma_MR.materials[1] = platforma_standard;
        begiai_MR.material = begiai_standard;
        laikikliai_MR.material = laikikliai_standard;                     
        vamzdis_MR.material = vamzdis_standard;
        vamzdzio_laikiklis_MR.material = vamzdzio_laikiklis_standard;
    }
}

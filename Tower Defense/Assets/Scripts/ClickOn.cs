using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOn : MonoBehaviour
{

    [SerializeField]
    private Material red;
    [SerializeField]
    private Material green;

    private MeshRenderer myRend;

    // Use this for initialization
    void Start ()
    {
        myRend = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickMe()
    {
        myRend.material = green;
    }
}

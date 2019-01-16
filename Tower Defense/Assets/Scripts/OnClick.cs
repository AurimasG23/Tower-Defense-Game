//attached to movable object
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour
{
    public int index;

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	//void Update () {
		
	//}

    public void SelectMe()
    {
        SelectAndMove.instance.SelectBuilding(index);
    }
}

//This script is attached to main camera
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    [SerializeField]
    private LayerMask movablesLayer;

	// Use this for initialization
	//void Start () {
		
	//}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetMouseButton(0))
        {
            RaycastHit rayHit;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, 
                movablesLayer, QueryTriggerInteraction.UseGlobal))
            {
                OnClick onClick = rayHit.collider.GetComponent<OnClick>();
                if(onClick)
                {
                    onClick.SelectMe();
                }
            }
        }
		
	}
}

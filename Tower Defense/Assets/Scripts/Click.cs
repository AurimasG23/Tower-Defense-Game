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
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit rayHit;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, movablesLayer, QueryTriggerInteraction.UseGlobal))
            {
                ClickOn con = rayHit.collider.GetComponent<ClickOn>();
                if(con)
                {
                    con.ClickMe();
                }
            }
        }
		
	}
}

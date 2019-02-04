//This script is attached to main camera
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    [SerializeField]
    private LayerMask movablesLayer;
    private float maxBuildingClickDuration = 0.4f;
    private float clickTime;

    private OnClick onClick;

    private bool mouseOverCanvaselement;

    // Use this for initialization
    void Start ()
    {
        mouseOverCanvaselement = false;
    }

    // Update is called once per frame
    void Update ()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit rayHit;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, 
                movablesLayer, QueryTriggerInteraction.UseGlobal))
            {
                clickTime = Time.timeSinceLevelLoad;
                onClick = rayHit.collider.GetComponent<OnClick>();               
            }
            else
            {
                SelectAndMove.instance.DeselectBuildings();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Time.timeSinceLevelLoad - clickTime <= maxBuildingClickDuration)
            {
                if (onClick)
                {
                    onClick.SelectMe();
                }
            }

        }
    }
}

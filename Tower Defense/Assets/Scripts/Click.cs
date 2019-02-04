//This script is attached to main camera
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour
{
    [SerializeField]
    private LayerMask movablesLayer;
    [SerializeField]
    private LayerMask movelessLayer;
    private float maxBuildingClickDuration = 0.3f;
    private float clickTime;

    private OnClick onClick;

    private bool deselect;

    // Use this for initialization
    void Start ()
    {
        deselect = false;
    }

    // Update is called once per frame
    void Update ()
    {
        if(!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
        {
            RaycastHit rayHit;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, 
                movablesLayer, QueryTriggerInteraction.UseGlobal))
            {
                clickTime = Time.timeSinceLevelLoad;
                onClick = rayHit.collider.GetComponent<OnClick>();
            }
            else if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity,
                movelessLayer, QueryTriggerInteraction.UseGlobal))
            {
                clickTime = Time.timeSinceLevelLoad;
                deselect = true;
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
                if(deselect)
                {
                    SelectAndMove.instance.DeselectBuildings();
                    deselect = false;
                }
            }

        }
    }
}

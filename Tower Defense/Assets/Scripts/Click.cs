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

    //------------------------

    private Vector3 Origin;
    private Vector3 Diference;
    private bool Drag = false;

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
                    onClick = null;
                }
                if(deselect)
                {
                    SelectAndMove.instance.DeselectBuildings();
                    deselect = false;
                }
            }
        }

        //-----------------------
        if (!BuildingPlacement.instance.onDrag)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f && Camera.main.orthographicSize > 12)
            {
                Camera.main.orthographicSize -= 1;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f && Camera.main.orthographicSize < 25)
            {
                Camera.main.orthographicSize += 1;
                Vector3 newPosition = new Vector3();
                if (Camera.main.transform.position.x < -20)
                {
                    newPosition = new Vector3(Camera.main.transform.position.x + 2, Camera.main.transform.position.y, Camera.main.transform.position.z);
                    Camera.main.transform.position = newPosition;
                }
                else if (Camera.main.transform.position.x < -15)
                {
                    newPosition = new Vector3(Camera.main.transform.position.x + 1, Camera.main.transform.position.y, Camera.main.transform.position.z);
                    Camera.main.transform.position = newPosition;
                }
                else if (Camera.main.transform.position.x > 10)
                {
                    newPosition = new Vector3(Camera.main.transform.position.x - 2, Camera.main.transform.position.y, Camera.main.transform.position.z);
                    Camera.main.transform.position = newPosition;
                }
                else if (Camera.main.transform.position.x > 0)
                {
                    newPosition = new Vector3(Camera.main.transform.position.x - 1, Camera.main.transform.position.y, Camera.main.transform.position.z);
                    Camera.main.transform.position = newPosition;
                }

                if (Camera.main.transform.position.z < -25)
                {
                    newPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 2);
                    Camera.main.transform.position = newPosition;
                }
                else if (Camera.main.transform.position.z < -18)
                {
                    newPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 1);
                    Camera.main.transform.position = newPosition;
                }
                else if (Camera.main.transform.position.z > -5)
                {
                    newPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z - 2);
                    Camera.main.transform.position = newPosition;
                }
                else if (Camera.main.transform.position.z > -10)
                {
                    newPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z - 1);
                    Camera.main.transform.position = newPosition;
                }
            }


            if (Input.GetMouseButton(0))
            {
                Diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
                if (Drag == false)
                {
                    Drag = true;
                    Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            else
            {
                Drag = false;
            }
            if (Drag == true)
            {
                Vector3 newCameraPosition = Origin - Diference;
                float xMin = -40 - ((20 - Camera.main.orthographicSize) * 2);
                float xMax = 30 + ((20 - Camera.main.orthographicSize) * 2);
                float zMin = -60 - ((20 - Camera.main.orthographicSize) * 2);
                float zMax = 20 + ((20 - Camera.main.orthographicSize) * 2);
                if (newCameraPosition.x < xMin || newCameraPosition.x > xMax)
                {
                    newCameraPosition.x = Camera.main.transform.position.x;
                }
                if (newCameraPosition.z < zMin || newCameraPosition.z > zMax)
                {
                    newCameraPosition.z = Camera.main.transform.position.z;
                }
                newCameraPosition.y = Camera.main.transform.position.y;

                Camera.main.transform.position = newCameraPosition;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private Transform selectedBuilding;
    private bool mouseButtonPressed;

    public static BuildingPlacement instance;

    // Use this for initialization
    void Start ()
    {
        instance = this;

        mouseButtonPressed = false;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mouseButtonPressed = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            mouseButtonPressed = false;
        }

        //if(Input.touchCount > 0)
        //      {
        //          Touch touch = Input.GetTouch(0);
        //          switch (touch.phase)
        //          {
        //              case TouchPhase.Began:
        //                  Vector3 m = touch.position;
        //                  break;
        //              case TouchPhase.Moved:
        //                  m = new Vector3(touch.position.x, touch.position.y, transform.position.y);
        //                  Vector3 pos = GetComponent<Camera>().ScreenToViewportPoint(m);
        //                  selectedBuilding.position = new Vector3(pos.x / 2, 0, pos.y * 5);
        //                  break;
        //              case TouchPhase.Ended:
        //                  break;
        //          }
        //      }


        if (SelectAndMove.instance.selectedBuildingIndex != -1 && mouseButtonPressed)
        {
            Vector3 m = Input.mousePosition;
            m = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.y);
            Vector3 pos = GetComponent<Camera>().ScreenToViewportPoint(m);
            selectedBuilding.position = new Vector3(pos.x / 2, 0.5f, pos.y * 5);           
        }

	}

    public void SetItem(GameObject building)
    {
        //selectedBuilding = (Instantiate(building, new Vector3(0, 0, 0), Quaternion.identity)).transform;
        selectedBuilding = building.transform;
        //GetComponent<Camera>().transform.position = new Vector3(-10, GetComponent<Camera>().transform.position.y, -10);
    }
}

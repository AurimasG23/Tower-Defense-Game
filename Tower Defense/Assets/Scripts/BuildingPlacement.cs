using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private Transform selectedBuilding;
    private bool mouseButtonPressedOnBuilding;

    [SerializeField]
    private LayerMask movablesLayer;
    private OnClick onClick;

    public static BuildingPlacement instance;

    // Use this for initialization
    void Start ()
    {
        instance = this;

        mouseButtonPressedOnBuilding = false;

    }
	
	// Update is called once per frame
	void Update ()
    {
        // check if mouse is pressed on selected building
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit rayHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity,
                movablesLayer, QueryTriggerInteraction.UseGlobal))
            {
                onClick = rayHit.collider.GetComponent<OnClick>();
                if (onClick)
                {
                    if(onClick.index == SelectAndMove.instance.selectedBuildingIndex)
                    {
                        mouseButtonPressedOnBuilding = true;
                    }
                }
            }          
        }
        if(Input.GetMouseButtonUp(0))
        {
            mouseButtonPressedOnBuilding = false;
        }    

        // jei yra pasirinktas pastatas ir ant jo paspausta pele, tuomet pagal pele keiciam pastato pozicija
        if (SelectAndMove.instance.selectedBuildingIndex != -1 && mouseButtonPressedOnBuilding)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo))
            {
                //selectedBuilding.position = new Vector3(hitInfo.point.x, 0.5f, hitInfo.point.z);
                SetBuildingPosition(hitInfo.point.x, hitInfo.point.z);
            }
        }

    }

    public void SetItem(GameObject building)
    {
        selectedBuilding = building.transform;
    }

    //padeda pastata i reikiama pozicija pagal grid
    private void SetBuildingPosition(float x, float z)
    {       
        float xValue;
        if(x >= 0)
        {
            xValue = Mathf.Floor(x) + 0.5f;
        }
        else
        {
            xValue = Mathf.Ceil(x) - 0.5f;
        }

        float zValue;
        if (z >= 0)
        {
            zValue = Mathf.Floor(z) + 0.5f;
        }
        else
        {
            zValue = Mathf.Ceil(z) - 0.5f;
        }

        selectedBuilding.position = new Vector3(xValue, 0.5f, zValue);
    }
}

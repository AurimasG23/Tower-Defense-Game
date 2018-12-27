//pasirinkto objekto pozicijos keitimo klasė. Ji užtikrina kad objektas judės pagal grida.
//dirbama tik su vienu(pasirinktu) objektu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private Transform selectedBuilding;              //pasirinktas pastatas
    private BuildingDimensions selectedBuildingDimensions;  //pasirinkto pastato matmenys
    private bool mouseButtonPressedOnBuilding;       //ar pele paspausta ant pastato

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
                if (hitInfo.point.x > -22 && hitInfo.point.x < 22 && hitInfo.point.z > -22 && hitInfo.point.z < 22)
                {
                    SetBuildingPosition(hitInfo.point.x, hitInfo.point.z);
                }
                else
                {
                    mouseButtonPressedOnBuilding = false;
                }
            }
        }

    }

    public void SetItem(GameObject building, BuildingDimensions dimensions)
    {
        selectedBuilding = building.transform;
        selectedBuildingDimensions = new BuildingDimensions(dimensions.xLength, dimensions.zWidth);
    }

    //padeda pastata i reikiama pozicija pagal grid
    private void SetBuildingPosition(float x, float z)
    {
        float xValue = 0;
        if (SelectAndMove.instance.buildingsDimensions[SelectAndMove.instance.selectedBuildingIndex].xLength % 2 == 1)
        {           
            if (x >= 0)
            {
                xValue = Mathf.Floor(x) + 0.5f;
            }
            else
            {
                xValue = Mathf.Ceil(x) - 0.5f;
            }
        }
        else
        {
            if (x >= 0)
            {
                if(x - Mathf.Floor(x) >= 0.5)
                {
                    xValue = Mathf.Ceil(x);
                }
                else
                {
                    xValue = Mathf.Floor(x);
                }
            }
            else
            {
                if(x - Mathf.Ceil(x) <= -0.5)
                {
                    xValue = Mathf.Floor(x);
                }
                else
                {
                    xValue = Mathf.Ceil(x);
                }                
            }
        }       

        float zValue = 0;
        if (SelectAndMove.instance.buildingsDimensions[SelectAndMove.instance.selectedBuildingIndex].zWidth % 2 == 1)
        {
            if (z >= 0)
            {
                zValue = Mathf.Floor(z) + 0.5f;
            }
            else
            {
                zValue = Mathf.Ceil(z) - 0.5f;
            }
        }
        else
        {
            if (z >= 0)
            {
                if (z - Mathf.Floor(z) >= 0.5)
                {
                    zValue = Mathf.Ceil(z);
                }
                else
                {
                    zValue = Mathf.Floor(z);
                }
            }
            else
            {
                if (z - Mathf.Ceil(z) <= -0.5)
                {
                    zValue = Mathf.Floor(z);
                }
                else
                {
                    zValue = Mathf.Ceil(z);
                }
            }
        }

        float boundsX = 20 - (selectedBuildingDimensions.xLength / 2) + 1;
        float boundsZ = 20 - (selectedBuildingDimensions.zWidth / 2) + 1;
        if (xValue > -boundsX && xValue < boundsX && zValue > -boundsZ && zValue < boundsZ)
        {
            selectedBuilding.position = new Vector3(xValue, 0, zValue);  //judinamo objekto pozicija
            SelectAndMove.instance.EmptySpot(SelectAndMove.instance.selectedBuildingIndex);
        }
    }
}

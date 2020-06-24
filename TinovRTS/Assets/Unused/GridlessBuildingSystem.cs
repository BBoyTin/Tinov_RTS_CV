using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridlessBuildingSystem : MonoBehaviour
{


    //skinuto s ovog linka
    // https://www.youtube.com/watch?v=KfCYR0yXGpI

    public enum CursorState { Building, Rotating }

    public CursorState state = CursorState.Building;
    public GameObject objToMove;
    public GameObject objToPlace;
    public LayerMask mask;
    GameObject builtObject;
    float LastPosX, LastPosY, LastPosZ;
    Vector3 mousePos;

    Structure structure;

    public bool placingObject = false;


    private void Start()
    {
        objToMove.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            objToMove.SetActive(true);
            placingObject = true;
        }

        if (placingObject == true)
        {
            PlaceObject();
        }
        


    }

    void PlaceObject()
    {
        mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            float PosX = hit.point.x;
            float PosY = hit.point.y;
            float PosZ = hit.point.z;

            if (PosX != LastPosX || PosY != LastPosY || PosZ != LastPosZ)
            {
                LastPosX = PosX;
                LastPosY = PosY;
                LastPosZ = PosZ;
                objToMove.transform.position = new Vector3(LastPosX, LastPosY + .5f, LastPosZ);
            }
            if (Input.GetMouseButton(0))
            {
                objToMove.SetActive(false);
                if (state == CursorState.Building)
                {
                    GameObject building = (GameObject)Instantiate(objToPlace,
                        objToMove.transform.position,
                        Quaternion.identity);

                    structure = building.GetComponent<Structure>();
                    builtObject = building;
                    state = CursorState.Rotating;
                }
                if (state == CursorState.Rotating)
                    builtObject.transform.LookAt(new Vector3(LastPosX, builtObject.transform.position.y, LastPosZ));
            }
            if (Input.GetMouseButtonUp(0))
            {
                objToMove.SetActive(true);
                if (!structure.okay)
                {
                    Destroy(builtObject);
                }
                else if (structure.okay)
                {
                    //FIX THIS
                    //structure.rend.material = structure.matDefault;
                    Destroy(structure);
                    placingObject = false;
                    objToMove.SetActive(false);
                }
                builtObject = null;
                state = CursorState.Building;
            }
        }
    }

}
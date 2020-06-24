using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBuildingSystem : MonoBehaviour
{
    public GameObject ObjToMove;
    public GameObject ObjToPlace;
    public LayerMask mask;
    int LastPosX, LastPosY, LastPosZ;
    Vector3 mousePos;

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            int PosX = (int)Mathf.Round(hit.point.x);
            int PosY = (int)Mathf.Round(hit.point.y);
            int PosZ = (int)Mathf.Round(hit.point.z);

            //Debug.Log("X: " + PosX + " & Z: " + PosZ);
            if (PosX != LastPosX || PosY != LastPosY || PosZ != LastPosZ)
            {
                LastPosX = PosX;
                LastPosY = PosY;
                LastPosZ = PosZ;
                ObjToMove.transform.position = new Vector3(PosX, PosY + .5f, PosZ);
            }
            if (Input.GetMouseButtonDown(0))
                Instantiate(ObjToPlace, ObjToMove.transform.position, Quaternion.identity);

        }
    }
}

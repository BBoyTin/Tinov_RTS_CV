using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUIClick : MonoBehaviour
{
    public LayerMask mask;
    Vector3 mousePos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickOnBuilding();
        }
    }

    void ClickOnBuilding()
    {

        mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            
            if (hit.collider.gameObject.GetComponent<BuildingInfo>().isActiveAndEnabled)
            {
                
                BuildingInfo bInfo = hit.collider.gameObject.GetComponent<BuildingInfo>();
                if (!bInfo.builidingUI.activeSelf)
                {
                    bInfo.builidingUI.SetActive(true);
                    
                }
            }
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{

    public LayerMask clickableLayer;

    [HideInInspector]
    public List<GameObject> selectedObjects;

    //ovu koristimo da gledamo objekte koje imaju na sebi skriptu IsSelected
    [HideInInspector]
    public List<GameObject> selectableObjects;

    Vector3 startPos;
    Vector3 endPos;
    
    public GameObject colliderBox;

    private void Awake()
    {
        selectedObjects = new List<GameObject>();
       
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                {
                    startPos = hit.point;
                    //startPos.y = 1f;
                    //Debug.Log(startPos);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                {
                    endPos = hit.point;
                    //startPos.y = 1f;
                    //Debug.Log(endPos);
                }
                BoxSelectObjects();
            }
           
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
                ClickToSelect();
        }
        
    }

    private void ClickToSelect()
    {
        RaycastHit rayHit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, clickableLayer))
        {
            IsSelected clickOnScript = rayHit.collider.GetComponent<IsSelected>();

            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (clickOnScript.currentlySelected == false)
                {
                    
                    clickOnScript.currentlySelected = true;
                    clickOnScript.ClickMe();
                }
                else
                {
                    selectedObjects.Remove(rayHit.collider.gameObject);
                    clickOnScript.currentlySelected = false;
                    clickOnScript.ClickMe();
                }
            }
            else
            {
                DeselectObjects();
                
                clickOnScript.currentlySelected = true;
                clickOnScript.ClickMe();
            }
        }

    }

    void DeselectObjects()
    {
        if (selectedObjects.Count > 0)
        {
            foreach (GameObject obj in selectedObjects)
            {
                obj.GetComponent<IsSelected>().currentlySelected = false;
                obj.GetComponent<IsSelected>().ClickMe();
            }
            selectedObjects.Clear();
        }

    }

    void BoxSelectObjects()
    {

        GameObject colliderBoxNew;

        List<GameObject> removeObjects = new List<GameObject>();
        if (Input.GetKey(KeyCode.LeftControl) == false)
        {
            DeselectObjects();
        }

        //nisam siguran jel ovdje ispravno, ali raid!!!
        DeselectObjects();

        Vector3 centreOfBox = new Vector3(startPos.x-(startPos.x - endPos.x) / 2f, 1f, startPos.z-(startPos.z - endPos.z) / 2f);

       
        colliderBoxNew = Instantiate(colliderBox, centreOfBox, Quaternion.identity);

        float sizeX = Mathf.Abs(startPos.x - endPos.x);
        float sizeZ = Mathf.Abs(startPos.z - endPos.z);
        colliderBoxNew.transform.localScale = new Vector3(sizeX, 5f, sizeZ);

        Destroy(colliderBoxNew, 0.1f);

        if (removeObjects.Count > 0)
        {
            foreach (GameObject rem in removeObjects)
            {
                selectableObjects.Remove(rem);
            }
            removeObjects.Clear();
        }
    }

    
}

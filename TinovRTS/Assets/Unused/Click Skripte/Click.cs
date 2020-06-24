using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://www.youtube.com/watch?v=23R3l8KQnts&t=5s


//SKRIPTA MORA BITI NA KAMERI DA RADI NORMALNO SVE, zbog ClickOn skripte

public class Click : MonoBehaviour
{

    public LayerMask clickableLayer;

    private List<GameObject> selectedObjects;

    public List<GameObject> selectableObjects;

    private Vector3 mousePos1;
    private Vector3 mousePos2;

    private void Awake()
    {
        selectedObjects = new List<GameObject>();
        selectableObjects = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickToSelect();
        }
        if (Input.GetMouseButtonUp(0))
        {
            mousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if(mousePos1 != mousePos2)
            {
                SelectBoxObjects();
            }
        }

        //ovu mou vezati na neki key ili gumb
        //DeselectObjects();
    }

    void ClickToSelect()
    {
        mousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        RaycastHit rayHit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, 100, clickableLayer))
        {
            ClickOn clickOnScript = rayHit.collider.GetComponent<ClickOn>();

            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (clickOnScript.currentlySelected == false)
                {
                    selectedObjects.Add(rayHit.collider.gameObject);
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
                selectedObjects.Add(rayHit.collider.gameObject);
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
                obj.GetComponent<ClickOn>().currentlySelected = false;
                obj.GetComponent<ClickOn>().ClickMe();
            }
            selectedObjects.Clear();
        }
        
    }

    void SelectBoxObjects()
    {
        List<GameObject> removeObjects = new List<GameObject>();

        if (Input.GetKey(KeyCode.LeftControl) == false)
        {
            DeselectObjects();
        }

        Rect selectRect = new Rect(mousePos1.x, mousePos1.y, mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y);

        foreach(GameObject selectObject in selectableObjects)
        {
            if(selectObject != null)
            {
                if (selectRect.Contains(Camera.main.WorldToViewportPoint(selectObject.transform.position), true))
                {
                    selectedObjects.Add(selectObject);
                    selectObject.GetComponent<ClickOn>().currentlySelected = true;
                    selectObject.GetComponent<ClickOn>().ClickMe();
                }
            }
            else
            {
                removeObjects.Add(selectObject);
            }
        }

        if(removeObjects.Count > 0)
        {
            foreach(GameObject rem in removeObjects)
            {
                selectableObjects.Remove(rem);
            }
            removeObjects.Clear();
        }
        

    }
}

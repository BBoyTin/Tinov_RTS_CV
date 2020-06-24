using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSelected : MonoBehaviour
{
    public bool currentlySelected = false;


    public GameObject selectionCircle;

    [HideInInspector]
    public GameObject circle;


    void Start()
    {

        CreateCircle();
        circle.SetActive(false);
        //ako ne zelimo da na kameri bude Click skripta ovdje moram mijenjati
        Camera.main.gameObject.GetComponent<SelectObject>().selectableObjects.Add(this.gameObject);

        ClickMe();
    }

    private void Update()
    {
        if (currentlySelected)
            return;
        if(circle.activeSelf && currentlySelected == false)
        {
            circle.SetActive(false);
        }
    }

    public void ClickMe()
    {
        if (currentlySelected == false)
        {
            circle.SetActive(false);
        }
        else
        {
            circle.SetActive(true);
            
            Camera.main.gameObject.GetComponent<SelectObject>().selectedObjects.Add(this.gameObject);
        }
    }

    private void CreateCircle()
    {
        

        //direkt kao djete
        //circle = Instantiate(selectionCircle, new Vector3(transform.position.x, 0.1f, transform.position.z), Quaternion.Euler(0, 0, 0), transform);

        
        //kada je objekt nakosen visina ovoga se clipa s podom
        //prvo napravimo onda instanciejtamo

        float avgSize = (transform.localScale.x + transform.localScale.z) / 2;
        circle = Instantiate(selectionCircle, new Vector3(transform.position.x, transform.position.y * 0.1f, transform.position.z), Quaternion.Euler(0, 0, 0));
        circle.transform.localScale = new Vector3(avgSize, 1, avgSize);
        circle.transform.parent = gameObject.transform;
        circle.transform.localRotation = Quaternion.Euler(0,0,0);
         
         
    }
}

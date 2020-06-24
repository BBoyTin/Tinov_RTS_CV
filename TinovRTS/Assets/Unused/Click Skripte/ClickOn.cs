using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOn : MonoBehaviour
{
    public bool currentlySelected = false;

    public Material red;
    public Material green;

    private MeshRenderer myRend;


    void Start()
    {
        myRend = GetComponent<MeshRenderer>();
        
        //ako ne zelimo da na kameri bude Click skripta ovdje moram mijenjati
        Camera.main.gameObject.GetComponent<Click>().selectableObjects.Add(this.gameObject);

        ClickMe();
    }

    public void ClickMe()
    {
        if (currentlySelected == false)
        {
            myRend.material = red;
        }
        else
        {
           myRend.material = green;
        }
    }
}

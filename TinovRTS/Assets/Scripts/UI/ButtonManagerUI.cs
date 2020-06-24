using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManagerUI : MonoBehaviour
{
    public GameObject[] listaObjnakonMainBaze;

    bool mainBaseFound = false;

    private void Update()
    {
        

        if (mainBaseFound)
            return;
        else if(GameObject.Find("MainBase(Clone)") != null)
        {
            mainBaseFound = true;
            for (int i = 0; i < listaObjnakonMainBaze.Length; i++)
            {
                listaObjnakonMainBaze[i].SetActive(true);
                
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilidingCanvasUI : MonoBehaviour
{
    
    public GameObject shopUI;

    public void ActivateDeactivateShop()
    {
        if(shopUI.activeSelf)
            shopUI.SetActive(false);
        else
            shopUI.SetActive(true);
    }

    
}

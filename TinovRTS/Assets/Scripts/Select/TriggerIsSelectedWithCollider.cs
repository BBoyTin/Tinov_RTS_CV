using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerIsSelectedWithCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger Enter");
        if (other.gameObject.layer == LayerMask.NameToLayer("SelectabolObject"))
        {
            //Debug.Log("if radi u OnTruggerEnter");
            other.gameObject.GetComponent<IsSelected>().currentlySelected = true;
            other.gameObject.GetComponent<IsSelected>().ClickMe();
        }
    }
    /*
    private void OnTriggerStay(Collider insideObj)
    {
        Debug.Log(" OnTriggerStay");
        if (insideObj.gameObject.layer == LayerMask.NameToLayer("SelectabolObject"))
        {
            Debug.Log("if radi u OnTriggerStay");
            insideObj.gameObject.GetComponent<IsSelected>().currentlySelected = true;
            insideObj.gameObject.GetComponent<IsSelected>().ClickMe();
        }
    }
    */
}

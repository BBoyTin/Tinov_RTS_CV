using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCollisionDetector : MonoBehaviour
{
    public bool isOkeyToPlace = false;

    private int objCount;



    void FixedUpdate()
    {
        if (objCount >= 1)
        {
           isOkeyToPlace = false;
           
        }
        else
        {
            isOkeyToPlace = true;
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        objCount++;
        //Debug.Log("broj colisiona " + objCount);
    }

    private void OnTriggerExit(Collider other)
    {
        objCount--;
        //Debug.Log("broj colisiona " + objCount);
    }

  
}

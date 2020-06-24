using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//od ocuda sam krenuo
//https://www.youtube.com/watch?v=KU2CKBlCAxQ

//koristim Nav Mesh Surface
//https://www.youtube.com/watch?v=CHV1ymlw-P8

public class MoveSelected : MonoBehaviour
{


    public LayerMask whatCanBeClickedOn;
    private NavMeshAgent myAgent;

    //sluzi da mozemo ga pomicati eventualno kasnije prek koda, ako zatreba
    [HideInInspector]
    public bool dopustenjeZaKretanje = false;

    
    //pomicem s desnim klikom

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        
        
    }

    void Update()
    {

        if (this.GetComponent<IsSelected>().currentlySelected == true)
        {
            MoveToMouseClicked();        
        }
        
        /*
        if (Input.GetKeyUp(KeyCode.C))
        {
            this.GetComponent<IsSelected>().currentlySelected = true;
        }
        */

    }

    void MoveToMouseClicked()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(myRay, out hitInfo, 100, whatCanBeClickedOn))
            {
                myAgent.SetDestination(hitInfo.point);
            }
        }
    }
}

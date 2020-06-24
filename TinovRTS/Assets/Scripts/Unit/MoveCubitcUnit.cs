using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MoveCubitcUnit : MonoBehaviour
{

    public LayerMask whatCanBeClickedOn;
    private NavMeshAgent agent;

    private Animator animator;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();


        animator.SetBool("isIDLE", true);



    }

    void Update()
    {

        if (this.GetComponent<IsSelected>().currentlySelected == true)
        {
            MoveToMouseClicked();
        }
        if (this.GetComponent<IsSelected>().currentlySelected == true && Input.GetKey(KeyCode.Y))
        {
            Work(true);
        }
        else
        {
            Work(false);
        }


    }

    private void Work(bool isAttk)
    {
        if (isAttk && animator.GetBool("isWorking"))
            return;

        if (isAttk)
            animator.SetBool("isWorking", true);
        else
            animator.SetBool("isWorking", false);

    }

    void MoveToMouseClicked()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(myRay, out hitInfo, Mathf.Infinity, whatCanBeClickedOn))
            {
                agent.SetDestination(hitInfo.point);
            }

            //Debug.Log("kretamo animacija");
            animator.SetBool("isMoving", true);

        }

        //krecemo
        if (agent.remainingDistance >= agent.stoppingDistance)
        {


            animator.SetBool("isMoving", true);

        }
        //blizu destinacije smo
        else
        {


            animator.SetBool("isIDLE", true);
            animator.SetBool("isMoving", false);


        }
    }
}

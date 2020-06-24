using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovePiramidUnit : MonoBehaviour
{
    public LayerMask whatCanBeClickedOn;
    private NavMeshAgent agent;

    private Animator animator;


    //ne treba, ali radi s ovim necu micati
    int check = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();


    }

    void Update()
    {

        if (this.GetComponent<IsSelected>().currentlySelected == true)
        {
            MoveToMouseClicked();
        }

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
            check = 0;
        }

        //krecemo
        if (agent.remainingDistance >= agent.stoppingDistance)
        {

            if (check == 0)
            {
                animator.SetBool("isMoving", true);
                //Debug.Log("krecemo se");
                check = 1;
            }
        }
        //blizu destinacije smo
        else
        {

            if (check == 1)
            {
                animator.SetBool("isIDLE", true);
                animator.SetBool("isMoving", false);
                //Debug.Log("stali smo");
                check = 0;
            }
        }
    }
}

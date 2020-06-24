using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    public string searchTag = "Building";

    public float range = 200f;

    Transform target=null;
    BuildingInfo buildingInfoScript;

    EnemyStats statsOfThis;
    private NavMeshAgent agent;
    private Animator animator;

    float stoppingDisBonus = 2f;
    


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        statsOfThis = GetComponent<EnemyStats>();

        animator.SetBool("isIDLE", true);
        //Debug.Log("idle animacija");

        

    }

    private void Update()
    {
        if (target == null)
        {
            UpdateTarget();
            return;
        }
            

        if (animator.GetBool("isMoving") == false && animator.GetBool("isAttacking") == false)
            MoveToTarget();
        //ako smo dovljno blizu da pucamo
        else if (agent.remainingDistance <= agent.stoppingDistance + stoppingDisBonus)
        {
            if (animator.GetBool("isAttacking") == false)
                StartCoroutine("AttackTarget");
            SmoothRoatationLookAt();
        }
        else
        {
            agent.SetDestination(target.position);

            SmoothRoatationLookAt();
        }
    }

    void UpdateTarget()
    {
        
       
        GameObject[] listOfObjs = GameObject.FindGameObjectsWithTag(searchTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;

        foreach (GameObject obj in listOfObjs)
        {
            float distanceToObj = Vector3.Distance(transform.position, obj.transform.position);
            if (distanceToObj < shortestDistance)
            {
                shortestDistance = distanceToObj;
                nearestTarget = obj;
            }
        }
        
        if (nearestTarget != null && shortestDistance <= range)
        {
            target = nearestTarget.transform;
            //buildingInfoScript = nearestTarget.GetComponent<BuildingInfo>();
            
        }
        else
        {
            target = null;
            SetAnimation(true, false, false);
        }
    }


    IEnumerator AttackTarget()
    {

        buildingInfoScript = target.GetComponent<BuildingInfo>();
        //sDebug.Log("building info start health "+buildingInfoScript.startHealth);
        agent.SetDestination(target.position);
        if (agent.remainingDistance < 0.5f)
        {
            target = null;
            yield break;
        }
        while (true)
        {
            if(buildingInfoScript == null)
            {
                target = null;
                SetAnimation(true, false, false);
                yield break;
            }
            if (agent.remainingDistance >= statsOfThis.attackRange + stoppingDisBonus)
            {
                target = null;
                SetAnimation(true, false, false);
                yield break;
            }

            SmoothRoatationLookAt();

            SetAnimation(false, false, true);

            buildingInfoScript.TakeDamage(statsOfThis.attackPower);

            yield return new WaitForSeconds(1f / statsOfThis.attackSpeed);
        }
    }

    private void MoveToTarget()
    {

        SetAnimation(false, true, false);

        agent.SetDestination(target.position);

        SmoothRoatationLookAt();
    }

    void SmoothRoatationLookAt()
    {
        if (target == null)
            return;
        //da gleda u target
        var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);

        // Smoothly rotate towards the target point 
        //hardkodiran speed ovdje
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);

        //gameObject.transform.LookAt(target.transform);
    }

    void SetAnimation(bool IDLE, bool Moving,bool Attacking)
    {
        animator.SetBool("isIDLE", IDLE);
        animator.SetBool("isMoving", Moving);
        animator.SetBool("isAttacking", Attacking);
    }
}

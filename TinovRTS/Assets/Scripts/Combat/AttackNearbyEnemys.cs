using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackNearbyEnemys : MonoBehaviour
{

    public string enemyTag="EnemyUnit";

    private Transform target;
    private EnemyStats enemyStatsScript;

    private NavMeshAgent agent;
    private Animator animator;
    private UnitStats statsOfThis;

    public float triggerRange = 15f;

    bool attackActivate = false;
    bool movingActivate = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        statsOfThis = GetComponent<UnitStats>();
        

        animator.SetBool("isIDLE", true);
        SetAnimation(false, false, true);

    }

    private void Update()
    {
        if (this.GetComponent<IsSelected>().currentlySelected == true)
            return;

        if (target == null)
        {
            UpdateTarget();
            SetAnimation(true, false, false);
            attackActivate = false;
            movingActivate = false;
            return;
        }


        if (target != null)
        {

            float distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToEnemy > triggerRange)
            {
                Debug.Log("distance to enemy " + distanceToEnemy);
                target = null;
                SetAnimation(true, false, false);
                return;
            }


            if (!movingActivate)
                agent.SetDestination(target.position);

            //ako smo dovljno blizu da pucamo
            if (agent.remainingDistance <= statsOfThis.attackRange + 0.5f)
            {
                if (!attackActivate)
                {
                    attackActivate = true;
                    
                    StartCoroutine("AttackTarget");
                    SmoothRoatationLookAt();
                   
                }

            }
            else
            {
                if (!movingActivate)
                {
                    movingActivate = true;
                    SetAnimation(false, true, false);
                }
                SmoothRoatationLookAt();
            }

        }


    }
    IEnumerator AttackTarget()
    {
        agent.SetDestination(target.position);
        if (agent.remainingDistance <= 0.1f)
        {
            target = null;
            yield break;
        }
           

        SetAnimation(false, false, true);
        if(target==null)
            yield break;

        enemyStatsScript = target.GetComponent<EnemyStats>();
        
        

        while (true)
        {
            if (enemyStatsScript == null)
            {
                target = null;
                SetAnimation(true, false, false);
                movingActivate = false;
                attackActivate = false;
                Debug.Log("nema enemya vise");
                yield break;
            }

            else if (agent.remainingDistance >= statsOfThis.attackRange + 1f)
            {
                target = null;
                SetAnimation(true, false, false);
                movingActivate = false;
                attackActivate = false;
                Debug.Log("enemy je predaleko za napadanje");
                yield break;
            }
            else if (agent.remainingDistance <= statsOfThis.attackRange + 1f)
            {
                SetAnimation(false, false, true);
                //Debug.Log("is attacking u while " + animator.GetBool("isAttacking"));

                enemyStatsScript.TakeDamage(statsOfThis.attackPower);

                yield return new WaitForSeconds(1f);
            }
           
        }
    }

    private void MoveToTarget()
    {

        SetAnimation(false, true, false);

        Debug.Log("is moving" + animator.GetBool("isMoving"));

        agent.SetDestination(target.position);

        SmoothRoatationLookAt();
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject obj in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, obj.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = obj;
            }
        }
        if (nearestEnemy != null && shortestDistance <= triggerRange)
        {
           
                target = nearestEnemy.transform;
       
        }
        else
        {
            target = null;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, triggerRange);
    }


    void SetAnimation(bool IDLE, bool Moving, bool Attacking)
    {
        animator.SetBool("isIDLE", IDLE);
        animator.SetBool("isMoving", Moving);
        animator.SetBool("isAttacking", Attacking);
        
    }

    void SmoothRoatationLookAt()
    {
        if (target == null)
            return;
        var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        //hardkodiran speed ovdje
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);

    }
}

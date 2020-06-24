using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAttack : MonoBehaviour
{
    private Transform target;
    private EnemyStats enemyStatsScript;

    [Header("General")]

    public float range = 25f;


    [Header("Use Laser")]
    public bool useLaser = true;
    public int damageOverTime = 10;
    //public float slowAmount = 0.5f;

    public LineRenderer lineRenderer;


    [Header("Setup Fields")]

    public string enemyTag = "EnemyUnit";

    public Transform firePoint;


    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 1f);
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if(target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                }
            }
            return;
        }

        if (useLaser)
        {
            Laser();
        }
    }

    private void Laser()
    {
        if (!lineRenderer.enabled)
            lineRenderer.enabled = true;

        enemyStatsScript.TakeDamage(damageOverTime * Time.deltaTime);

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
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
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            enemyStatsScript = nearestEnemy.GetComponent<EnemyStats>();
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

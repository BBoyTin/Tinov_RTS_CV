using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    public float startHealth = 50;

    [HideInInspector]
    public float health;

    public int attackPower = 5;
    public float attackSpeed = 1f;
    public float attackRange = 10f;


    public int speed = 5;

    public GameObject unitUI;
    public Image healthBar;

    [Header("UI Check")]
    public bool activeUI=false;



    private void Start()
    {
        health = startHealth;
        GetComponent<NavMeshAgent>().speed = speed;
        GetComponent<NavMeshAgent>().stoppingDistance = attackRange;
        if (!activeUI)
            unitUI.SetActive(false);

    }


 
    public void TakeDamage(float amount)
    {
        health = health - amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Destroy(gameObject);

        }
    }
}

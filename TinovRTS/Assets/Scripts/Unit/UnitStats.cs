using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class UnitStats : MonoBehaviour
{
    public float startHealth = 100;

    [HideInInspector]
    public float health;

    public int attackPower = 15;
    public float attackSpeed = 1f;
    public float attackRange = 2f;

    public int speed = 10;

    public GameObject unitUI;
    public Image healthBar;


    

    private void Start()
    {
        health = startHealth;
        GetComponent<NavMeshAgent>().speed = speed;
        unitUI.SetActive(false);

    }
    private void Update()
    {
        

        if (this.GetComponent<IsSelected>().currentlySelected == false)
        {
            if (!unitUI.activeSelf)
                return;
            unitUI.SetActive(false);
            return;
        }

        if (this.GetComponent<IsSelected>().currentlySelected == true)
        {
            if (unitUI.activeSelf)
                return;
            unitUI.SetActive(true);
            
        }
    }

    public void ClickBttnDamage()
    {
        float amount = 10f;
        health = health - amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Destroy(gameObject);
            //tu mogu dodati i particle system i svakaj nekaj

        }
    }

    public void TakeDamage(float amount)
    {
        health = health - amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Destroy(gameObject);
            //tu mogu dodati i particle system i svakaj nekaj

        }
    }
}

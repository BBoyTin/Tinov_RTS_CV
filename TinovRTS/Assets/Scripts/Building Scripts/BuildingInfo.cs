using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInfo : MonoBehaviour
{
    [Header("UI")]
    public GameObject builidingUI;
    public Image healthBar;

    [Header("General")]
    public float startHealth = 100;
    float health = 100;

    [Header("Upgrade")]
    public bool canUpgrade = false;

    public GameObject upgradePref;


    Vector3 mousePos;


    [HideInInspector]
    public bool isClicked = false;

    private void Start()
    {
        health = startHealth;
        builidingUI.SetActive(false);
    }


    public void TakeDamage(float amount)
    {

        health = health - amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Destroy(gameObject);
            //tu mogu dodati i particle system i svakaj nekaj
            Debug.Log("particle building exploison");
        }
    }
}

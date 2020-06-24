using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public string searchTag = "Unit";

    public float damageAmaount = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == searchTag)
        {
            Destroy(gameObject);

            //Debug.Log("we hit the unit " + other.gameObject.name);
            other.gameObject.GetComponent<UnitStats>().TakeDamage(damageAmaount);
        }
    }
}

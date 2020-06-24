using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletPlayer : MonoBehaviour
{
    public string searchTag = "EnemyUnit";

    public float damageAmaount = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == searchTag)
        {
            Destroy(gameObject);

            //Debug.Log("we hit the unit " + other.gameObject.name);
            other.gameObject.GetComponent<EnemyStats>().TakeDamage(damageAmaount);
        }
    }
}

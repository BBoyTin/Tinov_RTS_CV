using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject enemyPrefab;

    [Header("Spawn configure")]
    public float spawnInterval = 10f;
    public int numberOfObjSpawned = 2;

    public int numberOfSpawns = 10;

    public float inicalTimeBeforeSpawning = 35f;
    float offsetSpawn = 5;


    private void Start()
    {
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        int rounds = 0;
        yield return new WaitForSeconds(inicalTimeBeforeSpawning);

        while (rounds < numberOfSpawns)
        {
            for (int i = 0; i < numberOfObjSpawned; i++)
            {
                Vector3 offsetForSpawning = new Vector3(Random.Range(-offsetSpawn, offsetSpawn), 0, Random.Range(-offsetSpawn, offsetSpawn));
                Instantiate(enemyPrefab, transform.position + offsetForSpawning, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnInterval);
            rounds++;
        }

        Debug.Log("no more spawns left");
        yield break;
    }
}

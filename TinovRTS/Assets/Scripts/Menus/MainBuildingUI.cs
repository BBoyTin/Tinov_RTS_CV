using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBuildingUI : MonoBehaviour
{
    

    [Header("Prefabs")]
    public GameObject attackUnit;
    public int attackUnitCost = 35;

    public GameObject workerUnit;
    public int workerUnitCost = 15;

    [Header("Location")]
    public GameObject spawnPoint;
    public float offsetSpawn = 7f;

    GameObject gameManager;
    BuildingSystem buildingSystem;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        if (gameManager == null)
            Debug.Log("we miss a tag / or we miss the game Manager whole");
        buildingSystem = gameManager.GetComponent<BuildingSystem>();
    }


    public void SpawnAttacker()
    {
        Debug.Log("ToDo: money value here");
        if (buildingSystem.money < attackUnitCost)
            Debug.Log("need more money");
        else
        {
            buildingSystem.TakeMoney(attackUnitCost);
            Vector3 offsetForSpawning = new Vector3(Random.Range(-offsetSpawn, offsetSpawn), 0, 0);
            Instantiate(attackUnit, spawnPoint.transform.position + offsetForSpawning, Quaternion.identity);
        }
    }

    public void SpawnWorker()
    {
        Debug.Log("ToDo: money value here");
        if (buildingSystem.money < workerUnitCost)
            Debug.Log("need more money");
        else
        {
            buildingSystem.TakeMoney(workerUnitCost);
            Vector3 offsetForSpawning = new Vector3(Random.Range(-offsetSpawn, offsetSpawn), 0, 0);
            Instantiate(workerUnit, spawnPoint.transform.position + offsetForSpawning, Quaternion.identity);

        }
    }
}

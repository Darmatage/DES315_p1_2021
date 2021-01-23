using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AkshatMadanSpawner : MonoBehaviour
{
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;
    public GameObject bossObj;
    public GameObject door;
    private Door doorScript;
    public GameObject waveHandlerGO;
    private AkshatMadanWaveHandler waveHandler;
    public GameObject enemyText;
    public GameObject bossText;
    public int numEnemies = 0;
    public int maxEnemies = 5;
    public bool isDone = false;
    public GameObject spawner1;
    public GameObject spawner2;
    public GameObject spawner3;
    public GameObject spawner4;

    // Start is called before the first frame update
    void Start()
    {
        numEnemies = 0; // Start with 0 enemies on screen
        waveHandler = waveHandlerGO.GetComponent<AkshatMadanWaveHandler>(); // Get the wave handler
        isDone = false; // Set is done to false
        doorScript = door.GetComponent<Door>(); // Get the door script
    }

    // Update is called once per frame
    void Update()
    {
        enemyText.GetComponent<Text>().text = "ENEMIES: " + GameObject.FindGameObjectsWithTag("Enemy").Length.ToString() + " / " + maxEnemies.ToString(); // Update number of enemies on screen
        bossText.GetComponent<Text>().text = "BOSS HEALTH: " + bossObj.GetComponentInParent<MonsterMoveHit>().EnemyLives; // Get boss' health

        if (numEnemies == maxEnemies && !isDone)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) // If all enemies are dead
            {
                waveHandler.waveNumber += 1; // Wave number
                if (waveHandler.waveNumber > waveHandler.maxWaves)
                {
                    numEnemies = 0; // Reset number of enemies
                    isDone = true; // Waves are done, open door
                }
                else
                {
                    numEnemies = 0; // Reset number of enemies on screen
                }
            }
        }
        else if (numEnemies < maxEnemies && !isDone)
        {
            SpawnEnemy(); // Spawn an enemy
        }
        
        if(isDone)
        {
            waveHandler.waveNumber = waveHandler.maxWaves;
            maxEnemies = 0;
            bossObj.GetComponent<MonsterMoveHit>().EnemyLives = 1; // Decrease boss lives to 1
        }

        if (bossObj.GetComponent<MonsterMoveHit>().EnemyLives <= 1 && !isDone)
        {
            doorScript.DoorOpen(); // Open the door
            isDone = true;
        }
    }

    void SpawnEnemy()
    {
        if(Random.Range(0, 100) < 1) // 1/250 % of the time, spawn an enemy
        {
            int rand = Random.Range(0, 4); // Get a random number between 0 and 3
            GameObject spawner_copy;
            if (rand == 0)
                spawner_copy = spawner1;
            else if(rand == 1)
                spawner_copy = spawner2;
            else if (rand == 2)
                spawner_copy = spawner3;
            else
                spawner_copy = spawner4;

            int rand2 = Random.Range(0, 4);
            GameObject enemyClone;

            if (rand2 < 2)
                enemyClone = Instantiate(enemy1Prefab, new Vector3(spawner_copy.GetComponent<Transform>().position.x, spawner_copy.GetComponent<Transform>().position.y, spawner_copy.GetComponent<Transform>().position.z), new Quaternion(0, 0, 0, 0));
            else if (rand2 == 2)
                enemyClone = Instantiate(enemy2Prefab, new Vector3(spawner_copy.GetComponent<Transform>().position.x, spawner_copy.GetComponent<Transform>().position.y, spawner_copy.GetComponent<Transform>().position.z), new Quaternion(0, 0, 0, 0));
            else
                enemyClone = Instantiate(enemy3Prefab, new Vector3(spawner_copy.GetComponent<Transform>().position.x, spawner_copy.GetComponent<Transform>().position.y, spawner_copy.GetComponent<Transform>().position.z), new Quaternion(0, 0, 0, 0));
            enemyClone.tag = "Enemy"; // Tag them as enemy
            numEnemies++; // Increment number of enemies on screen
        }
    }
}

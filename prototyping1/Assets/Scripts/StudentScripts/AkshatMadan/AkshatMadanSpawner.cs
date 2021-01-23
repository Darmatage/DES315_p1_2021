using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AkshatMadanSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
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
        bossText.GetComponent<Text>().text = "BOSS HEALTH: " + this.GetComponentInParent<MonsterMoveHit>().EnemyLives; // Get boss' health

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
            doorScript.DoorOpen(); // Open the door
            Destroy(bossObj);
        }
    }

    void SpawnEnemy()
    {
        if(Random.Range(0, 250) < 1) // 1/250 % of the time, spawn an enemy
        {
            GameObject enemyClone = Instantiate(enemyPrefab, new Vector3(GetComponent<Transform>().position.x - 5.0f, GetComponent<Transform>().position.y, GetComponent<Transform>().position.z), new Quaternion(0, 0, 0, 0));
            enemyClone.tag = "Enemy"; // Tag them as enemy
            numEnemies++; // Increment number of enemies on screen
        }
    }
}

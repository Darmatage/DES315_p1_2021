using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DieterVoegels_SkeletonSpawner : MonoBehaviour
{
  [SerializeField] GameObject enemyReference;
  [SerializeField] GameObject playerReference;
  [SerializeField] GameObject doorReference;
  [SerializeField] GameObject switchReference;
  [SerializeField] string nextScene;
  [SerializeField] float enemySpeed;
  [SerializeField] int spawnRadius = 0;
  [SerializeField] int waveCount = 0;
  [SerializeField] int waveDuration = 0;
  [SerializeField] int enemyStartCount = 0;
  [SerializeField] int enemyCountIncrease = 0;

  [SerializeField] Text waveUIText;

  List<GameObject> skeletonArray = new List<GameObject>();
  float timer = 99;
  int enemyCount = 0;

  // Start is called before the first frame update
  void Start()
  {
    enemyCount = enemyStartCount;
    waveUIText.text = "Wave Count: " + waveCount;
  }

  // Update is called once per frame
  void Update()
  {
    timer += Time.deltaTime;

    if (timer >= waveDuration)
    {
      Debug.Log("New Wave");
      waveCount--;
      timer = 0;
      enemyCount += enemyCountIncrease;
      Debug.Log("Enemy Count: " + enemyCount);
      skeletonArray.Clear();

      if (waveCount < 0)
      {
        Debug.Log("Destroying Spawner");
        doorReference.SetActive(true);
        switchReference.SetActive(true);
        Destroy(gameObject);
        return;
      }

      Debug.Log("Spawning Enemies");
      for (int i = 0; i < enemyCount; i++)
      {
        Vector3 spawnPosition = GetComponent<Transform>().position;
        spawnPosition.x += Random.Range(-spawnRadius, spawnRadius + 1);
        spawnPosition.y += Random.Range(-spawnRadius, spawnRadius + 1);

        GameObject newEnemy = Instantiate(enemyReference, spawnPosition, Quaternion.identity);

        newEnemy.GetComponent<DieterVoegels_Skeleton>().playerReference = playerReference;
        newEnemy.GetComponent<DieterVoegels_Skeleton>().speed = enemySpeed;
        newEnemy.GetComponent<DieterVoegels_Skeleton>().timer = waveDuration - 1;
      }
    }

    waveUIText.text = "Wave Count: " + waveCount;
  }
}

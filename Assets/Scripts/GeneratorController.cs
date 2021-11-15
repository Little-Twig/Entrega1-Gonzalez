using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    enum Difficulties { Easy = 1, Normal, Hard };

    [SerializeField] private float startDelay = 2;
    [SerializeField] private float spawnInterval = 1.5f;
    [SerializeField] private Difficulties difficulty;
    [SerializeField] private GameObject[] enemyPrefabs;

    void Start()
    {
       switch (difficulty)
       {
           case Difficulties.Easy:
               InvokeRepeating("SpawnEnemy", (startDelay + 3f), (spawnInterval + 3f));
           break;
           case Difficulties.Normal:
               InvokeRepeating("SpawnEnemy", startDelay, spawnInterval);
           break;
           case Difficulties.Hard:
               InvokeRepeating("SpawnEnemy", (startDelay - 1f), (spawnInterval - 1f));
           break;
           default:
               Debug.Log("<color=#FF0000>ERROR AL ELEGIR NIVEL</color>");
               break;
       }

    }
    void Update()
    {
        
    }
    void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[enemyIndex], transform.position, enemyPrefabs[enemyIndex].transform.rotation);
    }

}

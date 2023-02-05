using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;

    [SerializeField] GameObject[] NPC = new GameObject[10];  //even the NPCs are enemies
    [SerializeField] GameObject[] Enemies = new GameObject[10];
    [SerializeField] GameObject[] MidBoss = new GameObject[10];
    [SerializeField] GameObject[] finalBoss = new GameObject[2];

    [SerializeField] GameObject boss;
    [SerializeField] GameObject[] midbossArray;
    [SerializeField] GameObject[] enemiesArray;
    [SerializeField] GameObject[] NPCArray;
    [SerializeField] private int NPCListLength;
    [SerializeField] private int enemyListLength;
    [SerializeField] private int midbossListLength;

    GameObject[] midbossspawnpoints;
    GameObject[] normalspawnpoints;
    GameObject[] finalspawnpoints;
    [SerializeField] private float timeToSpawn;


    private float tEnemySpawn = 100;
    private int enemyCounter = 0;


    public static EnemyManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);   //to keep the singleton between scenes


    }

    private void Start()
    {
        normalspawnpoints = GameObject.FindGameObjectsWithTag("normalSpawnpoint");
        midbossspawnpoints = GameObject.FindGameObjectsWithTag("MidBossSpawnpoint");
        finalspawnpoints = GameObject.FindGameObjectsWithTag("BossSpawnpoint");

        enemyListLength = normalspawnpoints.Length;
        NPCListLength = normalspawnpoints.Length;
        midbossListLength = midbossspawnpoints.Length;

        enemiesArray = new GameObject[enemyListLength];
        NPCArray = new GameObject[NPCListLength];
        midbossArray = new GameObject[midbossListLength];
        System.Random rnd = new System.Random();


        for (int i = 0; i < enemyListLength; i++)
        {
            double trs = rnd.NextDouble();
            if (trs > 0.5 && trs < 0.7)
            {
                int index = (int)(rnd.NextDouble() * Enemies.Length);
                enemiesArray[i] = GameObject.Instantiate(Enemies[index], normalspawnpoints[i].transform.position, normalspawnpoints[i].transform.rotation);
                enemiesArray[i].SetActive(true);
            }
            else if (trs > 0.8)
            {
                int index = (int)(rnd.NextDouble() * NPC.Length);
                NPCArray[i] = GameObject.Instantiate(NPC[index], normalspawnpoints[i].transform.position, normalspawnpoints[i].transform.rotation);
                NPCArray[i].SetActive(true);
            }
        }

        for (int i = 0; i < midbossListLength; i++)
        {
            midbossArray[i] = GameObject.Instantiate(MidBoss[i], midbossspawnpoints[i].transform.position, midbossspawnpoints[i].transform.rotation);
            midbossArray[i].SetActive(true);
        }

        for (int i = 0; i < finalspawnpoints.Length; i++)
        {
            boss = GameObject.Instantiate(finalBoss[0], finalspawnpoints[i].transform.position, finalspawnpoints[i].transform.rotation);
            boss.SetActive(true);
        }

        //StartCoroutine(SpawnEnemies());
    }

    //IEnumerator SpawnEnemies()
    //{
    //    while (true)
    //    {
    //        SpawnEnemy();
    //        yield return new WaitForSeconds(timeToSpawn);
    //    }
    //}

    //void SpawnEnemy()
    //{
    //    tEnemySpawn = 0;
    //    System.Random rnd = new System.Random();
    //    for (int i = enemyCounter; i < enemyListLength; i++)
    //    {
    //        if (!enemiesArray[i].activeInHierarchy)
    //        {
    //            enemiesArray[i].GetComponent<Enemy>().Spawn(normalspawnpoints[(int)(rnd.Next()*( spawnPoint.Length - 1)))]);
    //            enemiesArray[i].SetActive(true);

    //            enemyCounter = i + 1;
    //            break;
    //        }
    //        if (i == enemyListLength - 1)
    //            enemyCounter = 0;

    //    }
    //    if (enemyCounter >= enemyListLength)
    //    {
    //        enemyCounter = 0;
    //    }
    //}

    public void resetAll()
    {
        for (int i = 0; i < enemyListLength; i++)
        {
            enemiesArray[i].SetActive(false);
        }
    }


}

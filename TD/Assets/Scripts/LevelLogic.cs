using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLogic : MonoBehaviour
{
    public int enemySpawnCount;
    public int enemysToSpawn;
    public float enemySpeed = 3;
    public float enemyHealth = 2;
    public float enemyWaveHealthIncrease = 0.15f;
    public float lastEnemyWaveHealth;
    public int currentWave = 1;
    public float enemySpawnDelay;
    public int goldPerKill;
    public int goldIncreasePerWave;
    public GameObject enemySpawn;
    public GameObject spawnLocation;
    public GameObject lifeUI;
    public GameObject gameOver;
    public int lives = 30;
    public bool waveReady = false;

    public int currentGold = 300;
    public GameObject goldUI;

    public GameObject startButton;
    public List<GameObject> activeEnemies = new List<GameObject>();

    private int enemyNum;

    public enemyBehavior enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        lifeUI = GameObject.Find("LifeNum");
        lifeUI.GetComponent<TMPro.TextMeshProUGUI>().text = lives.ToString();
        gameOver = GameObject.Find("GameOver");
        startButton = GameObject.Find("StartButton");
        goldUI.GetComponent<TMPro.TextMeshProUGUI>().text = currentGold.ToString();
        lastEnemyWaveHealth = enemyHealth;
    }

    public float spawnTimer = 0;
    public int spawnedEnemy = 0;
    // Update is called once per frame
    void Update()
    {
        if (spawnedEnemy < enemysToSpawn && waveReady)
        {

            spawnTimer += Time.deltaTime;
            if (spawnTimer > enemySpawnDelay)
            {
                var newSpawn = Instantiate(enemySpawn, spawnLocation.transform.position, spawnLocation.transform.rotation);
                newSpawn.name = "enemy" + enemyNum;
                enemyScript = newSpawn.GetComponent<enemyBehavior>();
                enemyScript.enemyHealth = enemyHealth;
                enemyScript.speed = enemySpeed;
                enemyScript.enemyGoldValue = goldPerKill;
                activeEnemies.Add(newSpawn);
                enemyNum++;
                spawnedEnemy++;
                spawnTimer = 0;
            }
        }
        else
        {
            waveReady = false;
        }
    }

    public void LifeCounter(int amount)
    {
        lives += amount;
        if(lives < 1)
        {
            lives = 0;
            gameOver.GetComponent<TMPro.TextMeshProUGUI>().text = "GAME OVER";
        }
        //Debug.Log(lives);
        lifeUI.GetComponent<TMPro.TextMeshProUGUI>().text = lives.ToString();
    }

    public void cleanUpEnemyList(GameObject deadEnemy)
    {

        if(deadEnemy)
        {
            activeEnemies.Remove(deadEnemy);
        }

        for (int i = 0; i < activeEnemies.Count; i++)
        {
            if (activeEnemies[i] == null)
            {
                activeEnemies.RemoveAt(i);
            }
        }

        if (activeEnemies.Count < 1)
        {
            enemyHealth += enemyHealth * enemyWaveHealthIncrease;
            startButton.GetComponent<StartButton>().waveReady = true;
            spawnedEnemy = 0;
            goldPerKill += goldIncreasePerWave;
            Debug.Log("Wave " + currentWave + " Complete");
            currentWave++;
        }
    }

    public void modifyGold(int adjustedGold)
    {
        currentGold += adjustedGold;
        goldUI.GetComponent<TMPro.TextMeshProUGUI>().text = currentGold.ToString();
    }
}

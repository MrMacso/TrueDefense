using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject winScene;
    [SerializeField] private TextMeshProUGUI waveText;
    private AudioManager audioManager;
    private LevelHandler levelHandler;
    public GameObject finalWinScene;
    private float spawnTime = 8;
    private float currentTime;

    private float enemyAmount = 3;

    private float maxNumOfWaves = 3;
    private float currNumOfWaves = 0;

    private float totalDeadMonster;
    private float currentDeadMonster = 0;

    private bool isGameRunning = false;
    private bool isLevelComplete = false;
    private bool isLevelStarted = false;

    public enum Level { NONE, ONE, TWO, THREE };

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        levelHandler = FindObjectOfType<LevelHandler>();
        currentTime = spawnTime;
    }
    void Update()
    {
        if(isLevelStarted)
        {
            //LEVEL ONE
            if (levelHandler.GetLevelInInt() == (int)Level.ONE)
                SetupPortal(10,5,10);
            //LEVEL TWO
            if (levelHandler.GetLevelInInt() == (int)Level.TWO)
                SetupPortal(20,10,10);
            //LEVEL THREE
            if (levelHandler.GetLevelInInt() == (int)Level.THREE)
                SetupPortal(30,15,15);

            totalDeadMonster = enemyAmount * maxNumOfWaves;
            //RESET VALUES
            isLevelStarted = false;
            isLevelComplete = false;
            currNumOfWaves = 0;
            waveText.text = "Wave " + currNumOfWaves.ToString() + "/" + maxNumOfWaves.ToString();
        }
        if(isGameRunning)
        {
            //SUMMONS WAVES OF MONSTERS 
            if (currNumOfWaves < maxNumOfWaves)
            {
                if (currentTime <= 0f)
                {
                    StartCoroutine(SummonWave());
                    currentTime = spawnTime;
                }
            }
            currentTime -= Time.deltaTime;
            //IF ALL MONTERS KILLED PLAY VICTORY WINDOW
            if (currentDeadMonster == totalDeadMonster && !isLevelComplete && levelHandler.GetLevelInInt() != (int)Level.THREE)
            {
                //sound
                audioManager.Play("Victory");
                //activate win window
                WinScene(true);
                //reset variables
                isLevelComplete= true;
                isGameRunning = false;
                currentDeadMonster = 0;
            }
            //if on the last level all monsters killed, activate Final Victory window
            if (currentDeadMonster == totalDeadMonster && !isLevelComplete && levelHandler.GetLevelInInt() == (int)Level.THREE)
            {
                //sound
                audioManager.Play("Victory");
                //activate final win window
                FinalWinScene();
                isLevelComplete = true;
                isGameRunning = false;
            }
        }
    }
   IEnumerator SummonWave()
    {
        //ADD +1 TO WAVE
        currNumOfWaves++;
        waveText.text = "Wave: " + currNumOfWaves.ToString() + "/" + maxNumOfWaves.ToString();
        //SPAWN ENEMIES WITH 1 SEC DELAY
        for (int i=0; i < enemyAmount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1.0f);
        }
    }
    private void SpawnEnemy()
    {
        Instantiate(enemy, transform.position, transform.rotation);
    }
    private void SetupPortal(int spawnT, int eAmount, int maxWave)
    {
         //SETUP VALUES FOR NEXT LEVEL
         spawnTime = spawnT;
         enemyAmount = eAmount;
         maxNumOfWaves = maxWave;
    }
    private void WinScene(bool isactive)
    {
        winScene.SetActive(isactive);
    }
    public void AddToDeadCounter()
    {
        currentDeadMonster++;
    }
    public void SetIsGameRunning(bool isrunning)
    {
        isGameRunning = isrunning;
    }
    public void StartLevel()
    {
        isLevelStarted= true;
    }
    public void FinalWinScene()
    {
        finalWinScene.SetActive(true);
    }
}

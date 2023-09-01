using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Transform> GroundSpawnPoints = new List<Transform>();
    public List<Transform> SkySpawnPoints = new List<Transform>();
    public GameObject EnemyTypeA;
    public GameObject EnemyTypeB;
    public GameObject EnemyTypeC;
    public GameObject EnemyTypeD;
    // The time between each enemy spawn
    /*public float TimeToSpawn = 1.5f;
    // This is to speed up the spawning
    public float SpawnTimeDecrease;
    // The probability of an enemy spawning
    public int [] EnemySpawnProbability;
    // This is the amount of times an enemy will spawn before its percentage gets decreased
    public int[] SpawnAmountTillPercentageChange;
    // This is the amount of times a certain enemy has spawned
    List<int> SpawnAmountForEnemies = new List<int>();
    // This is the percentage that will decrease the percentage of the chosen enemy and
    // increase the percentage of the other
    public int[] EnemyPercentageChange;
    // This is the minimum amount the enemie's percentage will decrease to
    public int[] EnemyPercetageMinimum;
    int TotalSpawnedEnemies = 0;*/
    public float TimeUntilEnemyTypeBSpawn;
    public float TimeUntilEnemyTypeCSpawn;
    public float TimeUntilEnemyTypeDSpawn;

    //EnemyTypeA
    public float EnemyTypeATime = 3;
    float ActualEnemyTypeATime;
    float RealTimer;
    float EnemyTypeATotal;

    //EnemyTypeB
    public float EnemyTypeBTime = 3;
    float ActualEnemyTypeBTime;
    float EnemyTypeBTotal;

    //EnemyTypeC
    public float EnemyTypeCTime = 3;
    float ActualEnemyTypeCTime;
    float EnemyTypeCTotal;
    
    //EnemyTypeD
    public float EnemyTypeDTime = 3;
    float ActualEnemyTypeDTime;
    float EnemyTypeDTotal;


    void Start()
    {
        /*StartCoroutine(DelaySpawn());
        foreach(var Enemy in EnemySpawnProbability)
        {
            SpawnAmountForEnemies.Add(0);
        }*/
        ActualEnemyTypeATime = EnemyTypeATime;
        ActualEnemyTypeBTime = .1f;
        ActualEnemyTypeCTime = .1f;
        ActualEnemyTypeDTime = .1f;
        EnemyTypeATotal = 0;
        EnemyTypeBTotal = 0;
        EnemyTypeCTotal = 0;
        EnemyTypeDTotal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RealTimer += Time.deltaTime;

        ActualEnemyTypeATime -= Time.deltaTime;
        if(ActualEnemyTypeATime <= 0)
        {
            SpawnEnemyTypeA();
        }

        if (RealTimer < TimeUntilEnemyTypeBSpawn) return;

        ActualEnemyTypeBTime -= Time.deltaTime;
        if (ActualEnemyTypeBTime <= 0)
        {
            SpawnEnemyTypeB();
        }

        if (RealTimer < TimeUntilEnemyTypeCSpawn) return;

        ActualEnemyTypeCTime -= Time.deltaTime;
        if (ActualEnemyTypeCTime <= 0)
        {
            SpawnEnemyTypeC();
        }
        if (RealTimer < TimeUntilEnemyTypeDSpawn) return;

        /*ActualEnemyTypeCTime -= Time.deltaTime;
        if (ActualEnemyTypeBTime <= 0)
        {
            SpawnEnemyTypeB();
        }*/
    }
    void SpawnEnemyTypeA()
    {
        ActualEnemyTypeATime = EnemyTypeATime;
        EnemyTypeATotal++;
        Instantiate(EnemyTypeA, GroundSpawnPoints[Random.Range(0, GroundSpawnPoints.Count)]);
    }
    void SpawnEnemyTypeB()
    {
        ActualEnemyTypeBTime = EnemyTypeBTime;
        EnemyTypeBTotal++;
        Instantiate(EnemyTypeB, SkySpawnPoints[Random.Range(0, SkySpawnPoints.Count)]);
    }
    void SpawnEnemyTypeC()
    {
        ActualEnemyTypeCTime = EnemyTypeCTime;
        EnemyTypeCTotal++;
        Instantiate(EnemyTypeC, GroundSpawnPoints[Random.Range(0, GroundSpawnPoints.Count)]);
    }
    void SpawnEnemyTypeD()
    {
        ActualEnemyTypeDTime = EnemyTypeDTime;
        EnemyTypeDTotal++;
        Instantiate(EnemyTypeD, GroundSpawnPoints[Random.Range(0, GroundSpawnPoints.Count)]);
    }

    /*IEnumerator DelaySpawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(TimeToSpawn);
            SpawnEnemy();
        }
    }
    void SpawnEnemy()
    {
        int RandomEnemyInt = Random.Range(0, 100);
        int low;
        int high = 0;
        for (int i = 0; i < EnemySpawnProbability.Length; i++)
        {
            low = high;
            high += EnemySpawnProbability[i];
            print(low);
            if (RandomEnemyInt >= low && RandomEnemyInt < high)
            {
                // Enemy Type A
                if (i == 0)
                {
                    Instantiate(EnemyTypeA, GroundSpawnPoints[Random.Range(0, GroundSpawnPoints.Count)]);
                    if (SpawnAmountForEnemies[i] >= SpawnAmountTillPercentageChange[i] - 1)
                    {
                        print("Change");
                        if (EnemySpawnProbability[i] > EnemyPercetageMinimum[i])
                        {
                            EnemySpawnProbability[i] -= EnemyPercentageChange[i];
                            EnemySpawnProbability[i + 1] += EnemyPercentageChange[i];
                            SpawnAmountForEnemies[i] = 0;
                        }
                        else
                        {
                            EnemySpawnProbability[i] = EnemyPercetageMinimum[i];
                            EnemySpawnProbability[i + 1] = EnemyPercetageMinimum[i + 1];
                        }
                    }
                }
                // Enemy Type B
                else if(i == 1)
                {
                    Instantiate(EnemyTypeB, SkySpawnPoints[Random.Range(0, SkySpawnPoints.Count)]);
                    if (EnemySpawnProbability[i] > EnemyPercetageMinimum[i] - 1)
                    {
                        EnemySpawnProbability[i] -= EnemyPercentageChange[i];
                        EnemySpawnProbability[i + 1] += EnemyPercentageChange[i];
                        SpawnAmountForEnemies[i] = 0;
                    }
                    else
                    {
                        EnemySpawnProbability[i] = EnemyPercetageMinimum[i];
                        EnemySpawnProbability[i + 1] = EnemyPercetageMinimum[i + 1];
                    }
                }
                // Enemy Type C
                else if(i == 2)
                {
                    Instantiate(EnemyTypeC, GroundSpawnPoints[Random.Range(0, GroundSpawnPoints.Count)]);
                    if (EnemySpawnProbability[i] > EnemyPercetageMinimum[i] - 1)
                    {
                        EnemySpawnProbability[i] -= EnemyPercentageChange[i];
                        EnemySpawnProbability[i + 1] += EnemyPercentageChange[i];
                        SpawnAmountForEnemies[i] = 0;
                    }
                    else
                    {
                        EnemySpawnProbability[i] = EnemyPercetageMinimum[i];
                    }
                }
                SpawnAmountForEnemies[i]++;
            }
        }
    }*/
}
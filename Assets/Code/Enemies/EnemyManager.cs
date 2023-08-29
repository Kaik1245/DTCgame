using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Transform> SpawnPoints = new List<Transform>();
    public GameObject EnemyTypeA; 
    public float TimeToSpawn = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelaySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Spawn()
    {
        if(SpawnPoints.Count != 0)
        {
            Instantiate(EnemyTypeA, SpawnPoints[Random.Range(0, SpawnPoints.Count)]);
        }
    }
    IEnumerator DelaySpawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(TimeToSpawn);
            Spawn();
        }
    }
}

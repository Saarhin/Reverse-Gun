using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject doorRef;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] float spawnRate;//every second
    float timer = 0;
    [SerializeField] int enemyCountInStart;
    float lastTimeSpawned = 0;

    public void FirstSpawn()
    {
        for(int i =0;i<enemyCountInStart;i++)
        {
            Spawn(true);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnRate + lastTimeSpawned)
        {
            lastTimeSpawned = timer;
            if (GameManager.instance.floorNumber <= 2)
            {
                Spawn(true);
            }
            else
                Spawn(false);
        }
    }
    void Spawn(bool isStartSpawn)
    {
        float side = (Random.Range(0, 2) - 0.5f) * 2f;
        GameObject instance;
        if (isStartSpawn)
            instance =Instantiate(enemyPrefabs[Random.Range(0,enemyPrefabs.Length)], doorRef.transform.position , Quaternion.identity, null);
        else
            instance =Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], (Vector2)FindObjectOfType<Camera>().transform.position + (Vector2.right * side * 10) + Vector2.up * Random.Range(-FindObjectOfType<Camera>().orthographicSize / 2, FindObjectOfType<Camera>().orthographicSize / 2), Quaternion.identity, null);
        instance.GetComponent<EnemyBehaviour>().Initialize(side);
        if (Mathf.Approximately(side, 1))
            instance.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}

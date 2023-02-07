using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlatformsSpawner : MonoBehaviour
{
    Queue<GameObject> spawnedPlatforms = new Queue<GameObject>();

    public GameObject platform;

    [Range(0f, 1f)]
    public float chanceOfEachFloor;

    public int maxFloorSpawn;

    private int currentFloorNumber;
    private int _currentFloorsSpawnChecked;
    private int currentFloorsSpawnChecked
    {
        get
        {
            return _currentFloorsSpawnChecked;
        }
        set
        {
            _currentFloorsSpawnChecked = value;
            if (_currentFloorsSpawnChecked < maxFloorSpawn)
            {
                Spawn();
            }
        }
    }

    private void Awake()
    {
        currentFloorsSpawnChecked = 0;
    }

    private void Update()
    {
        //Debug.Log(spawnedPlatforms.Count);

        if (spawnedPlatforms.Count != 0)
        {
            if (spawnedPlatforms.Peek().transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize - 1)
            {
                GameObject platform = spawnedPlatforms.Dequeue();
                Destroy(platform);
                currentFloorsSpawnChecked--;
            }
            //Debug.Log(Camera.main.orthographicSize);
        }

    }

    public void Spawn()
    {
        float spawnChance = Random.Range(0f, 1f);
        //Debug.Log("Floor: " + currentFloorNumber + " SpawnChance: " + spawnChance);
        currentFloorNumber++;
        if (spawnChance < chanceOfEachFloor)
        {
            int xPosition = Random.Range(-1, 2) * 2;
            int yPosition = currentFloorNumber * 2;
            GameObject buildingPlatform = Instantiate(platform, new Vector3(xPosition, yPosition, 0), Quaternion.identity);

            spawnedPlatforms.Enqueue(buildingPlatform);
            currentFloorsSpawnChecked++;
        }
        else
        {
            currentFloorsSpawnChecked--;
        }


    }
}

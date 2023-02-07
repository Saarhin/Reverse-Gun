using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlatformsSpawner : MonoBehaviour
{
    public GameObject platform;

    public float spawnDelay;
    float timer;

    private void Awake()
    {
        timer = spawnDelay;
    }

    private void Update()
    {
        if (timer < spawnDelay)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            Spawn();
        }
    }

    public void Spawn()
    {
        float xPosition = Random.Range(transform.position.x - (transform.localScale.x / 2) + (platform.transform.localScale.x ), transform.position.x + (transform.localScale.x / 2) - (platform.transform.localScale.x ));
        Instantiate(platform, new Vector3(xPosition, transform.position.y, 0), Quaternion.identity);
    }

}

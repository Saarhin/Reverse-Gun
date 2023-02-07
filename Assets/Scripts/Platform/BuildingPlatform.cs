using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlatform : MonoBehaviour
{
    Collider2D platformCollider;
    GameObject player;

    private void Awake()
    {
        platformCollider = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        platformCollider.isTrigger = true;
    }

    private void FixedUpdate()
    {
        if (transform.position.y < player.transform.position.y - player.transform.localScale.y / 2)
        {
            platformCollider.isTrigger = false;
        }
        else
            platformCollider.isTrigger = true;
    }
}

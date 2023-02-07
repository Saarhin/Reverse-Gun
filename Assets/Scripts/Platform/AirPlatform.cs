using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlatform : MonoBehaviour
{
    Collider2D platformCollider;
    GameObject player;

    public float speed;

    bool isActivated;
    bool isMoving;

    private void Awake()
    {
        isMoving = true;
        platformCollider = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        //Destroy(gameObject, 3);
    }

    private void FixedUpdate()
    {
        if (isActivated)
        {
            if (transform.position.y < player.transform.position.y - player.transform.localScale.y / 2)
            {
                platformCollider.isTrigger = false;
            }
            else
                platformCollider.isTrigger = true;
        }

        if (isMoving)
            transform.Translate(new Vector3(0f, -speed * Time.fixedDeltaTime, 0f));

        //Debug.Log(Mathf.Clamp(Mathf.Log(Time.time, 10), 1, 3));
    }


    public void Activate()
    {
        isActivated = true;
        isMoving = false;

        Color color = GetComponent<SpriteRenderer>().color;
        color.a = 255;
        GetComponent<SpriteRenderer>().color = color;

        gameObject.layer = LayerMask.NameToLayer("Walkable");

        AudioManager.instance.Play("AcitivatePlatform");
    }

}

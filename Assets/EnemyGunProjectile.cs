using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunProjectile : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    internal Vector2 targetPosition;

    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        if (Vector2.SqrMagnitude((Vector2)transform.position - targetPosition) < 0.5f)
        {
            gameObject.layer = LayerMask.NameToLayer("Bullet");
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            Destroy(gameObject, 0.1f);
        }
    }
}

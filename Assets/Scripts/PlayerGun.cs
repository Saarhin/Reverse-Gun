using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] GameObject gunEnd;
    [SerializeField] GameObject gunRef;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed;
    [SerializeField] float fireCooldown;

    float timer;
    float lastTimeFired;
    //float desiredAngle;
    //[SerializeField] float armMovementSmoothAmount;

    private void Update()
    {
        timer += Time.deltaTime;
        float angle = Vector2.SignedAngle(Vector2.right, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        if (Vector2.SqrMagnitude(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position) > 1f)
            transform.rotation = Quaternion.Euler(0, 0, angle);
        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            gunRef.GetComponent<SpriteRenderer>().flipY = true;
            GetComponentInParent<SpriteRenderer>().flipX = false;
        }
        else
        {
            gunRef.GetComponent<SpriteRenderer>().flipY = false;
            GetComponentInParent<SpriteRenderer>().flipX = true;
        }

        if (Input.GetButton("Fire1") && lastTimeFired + fireCooldown < timer && Vector2.SqrMagnitude(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position) > 1f && Vector2.SqrMagnitude(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position) < GameManager.instance.reverseGunFiringRange * GameManager.instance.reverseGunFiringRange)
        {
            lastTimeFired = timer;
            Shoot();
        }
        /*
        desiredAngle = Vector2.SignedAngle(Vector2.right, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        if (Vector2.SqrMagnitude(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position) > 1f)
        {
            if(desiredAngle <0)
            {
                desiredAngle += 360;
            }
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(transform.eulerAngles.z, desiredAngle, armMovementSmoothAmount));
        }
        else
        {
            desiredAngle = 0;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(transform.eulerAngles.z, desiredAngle, armMovementSmoothAmount));
        }
        */
    }

    void Shoot()
    {
        AudioManager.instance.Play("Shooting");

        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gunEnd.transform.position);
        GameObject projectile = Instantiate(projectilePrefab, gunEnd.transform.position, Quaternion.Euler(0,0, Vector2.SignedAngle(Vector2.right, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)), null);
        projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * projectileSpeed;
        projectile.GetComponent<ReverseGunProjectile>().targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}

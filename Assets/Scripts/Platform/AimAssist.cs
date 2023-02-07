using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAssist : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<ReverseGunProjectile>() != null)
        {
            transform.parent.GetComponent<AirPlatform>().Activate();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBrick : MonoBehaviour
{
    Vector3 initialPos;
    Quaternion initialRotation;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Building");
        initialPos = transform.position;
        initialRotation = transform.rotation;
    }

    public void Break(Vector2 contactPoint)
    {
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            Vector2 direction = -(contactPoint - (Vector2)transform.position);
            rb.AddForce(direction.normalized * GetComponentInParent<BuildingElement>().brickBreakForce, ForceMode2D.Force);
            rb.AddTorque(Random.Range(-1f, 1f) * GetComponentInParent<BuildingElement>().brickMaximumTorque);
        }
    }

    public IEnumerator Repair()
    {
        Destroy(GetComponent<Rigidbody2D>());
        while (Vector2.SqrMagnitude(transform.position - initialPos) > 0.01f)
        {
            transform.position = Vector2.Lerp(transform.position, initialPos, GetComponentInParent<BuildingElement>().repairSmoothAmount);
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(transform.rotation.eulerAngles.z, initialRotation.eulerAngles.z, GetComponentInParent<BuildingElement>().repairSmoothAmount));
            yield return null;
        }
        transform.position = initialPos;
        transform.rotation = initialRotation;
        gameObject.AddComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}

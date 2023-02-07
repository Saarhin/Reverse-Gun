using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float positiveHorizontalAcceleration;
    [SerializeField] float negativeHorizontalAcceleration;
    [SerializeField] float positiveVerticalAcceleration;
    [SerializeField] float negativeVerticalAcceleration;
    [SerializeField] float maxHorizontalSpeed;
    [SerializeField] float maxVerticalSpeed;

    [SerializeField] float stayTimeToBeConsideredToBeAtTheTargetPos;

    [SerializeField] float rangeToBeConsideredToBeAtTheTargetPos;
    [SerializeField] Vector2 currentTargetPos;
    internal bool isAtTargetPos = false;

    Rigidbody2D rb;

    bool isBreakingHorizontal = false;
    bool isBreakingVertical = false;

    float timer1 = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    bool flag = false;
    private void FixedUpdate()
    {

        if (currentTargetPos.x - transform.position.x * rb.velocity.x < 0)
        {
            isBreakingHorizontal = true;
        }
        else
            isBreakingHorizontal = false;

        if (currentTargetPos.y - transform.position.y * rb.velocity.y < 0)
        {
            isBreakingVertical = true;
        }
        else
            isBreakingVertical = false;

        if (Vector2.SqrMagnitude(currentTargetPos - (Vector2)transform.position) < rangeToBeConsideredToBeAtTheTargetPos * rangeToBeConsideredToBeAtTheTargetPos)
        {
            timer1 += Time.fixedDeltaTime;
            if (timer1 > stayTimeToBeConsideredToBeAtTheTargetPos && !flag)
            {
                flag = true;
                isAtTargetPos = true;
                rb.velocity = Vector2.zero;
                GetComponent<EnemyBehaviour>().StartCoroutine(GetComponent<EnemyBehaviour>().Shoot());
            }
        }
        else
        {
            flag = false;
            timer1 = 0;
            isAtTargetPos = false;
        }

        if (!isAtTargetPos)
        {
            float horizontalSpeedChange = Mathf.Sign(currentTargetPos.x - transform.position.x) * (isBreakingHorizontal ? negativeHorizontalAcceleration : positiveHorizontalAcceleration) * Time.fixedDeltaTime;
            float verticalSpeedChange = Mathf.Sign(currentTargetPos.y - transform.position.y) * (isBreakingVertical ? negativeVerticalAcceleration : positiveVerticalAcceleration) * Time.fixedDeltaTime;
            if (isBreakingHorizontal || Mathf.Abs(rb.velocity.x) < maxHorizontalSpeed)
                rb.velocity += Vector2.right * horizontalSpeedChange;
            if (isBreakingVertical || Mathf.Abs(rb.velocity.y) < maxVerticalSpeed)
                rb.velocity += Vector2.up * verticalSpeedChange;

            if (Mathf.Abs(rb.velocity.x) > maxHorizontalSpeed)
            {
                rb.velocity = Vector2.up * rb.velocity.y + Vector2.right * Mathf.Sign(rb.velocity.x) * maxHorizontalSpeed;
            }
            if (Mathf.Abs(rb.velocity.y) > maxVerticalSpeed)
            {
                rb.velocity = Vector2.right * rb.velocity.x + Vector2.up * Mathf.Sign(rb.velocity.y) * maxVerticalSpeed;
            }
        }
    }
    public void Move(Vector2 position)
    {
        currentTargetPos = position;
    }
}



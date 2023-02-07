using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterMovement : MonoBehaviour
{

    [SerializeField] float horizontalSpeed;
    [SerializeField] float airAcceleration;
    [SerializeField] float jumpDuration;
    [SerializeField] float jumpSpeed;
    [SerializeField] float jumpCooldown;
    [SerializeField] GameObject groundRef;
    internal float charecterHeight = 1;

    Rigidbody2D rb;
    bool isGrounded;
    bool isJumping;
    float timer=0;
    float timer2=0;
    float lastTimeJumped;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        timer2 += Time.fixedDeltaTime;
        if (isJumping)
        {
            timer += Time.fixedDeltaTime;
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                rb.velocity = rb.velocity * Vector2.right + Vector2.up * jumpSpeed;
            }
            else
            {
                rb.velocity = rb.velocity * Vector2.right;
                isJumping = false;
                timer = 0;
            }
            if (timer > jumpDuration)
            {
                isJumping = false;
                timer = 0;
            }
        }
        isGrounded = Physics2D.OverlapCircle(groundRef.transform.position, 0.1f, 1 << LayerMask.NameToLayer("Walkable"));
        if (isGrounded)
        {
            rb.velocity = rb.velocity * Vector2.up + Vector2.right * Input.GetAxisRaw("Horizontal") * horizontalSpeed;
            if(Input.GetAxisRaw("Horizontal") != 0)
                GetComponent<Animator>().SetBool("isWalking", true);
            else
                GetComponent<Animator>().SetBool("isWalking", false);
            if (Input.GetKeyDown("w") && lastTimeJumped + jumpCooldown < timer2 )
            {
                AudioManager.instance.Play("Jump");

                isJumping = true;
                lastTimeJumped = timer2;
                rb.velocity = rb.velocity * Vector2.right + Vector2.up * jumpSpeed;
            }
        }
        else
        {
            if (Mathf.Abs(rb.velocity.x) < horizontalSpeed)
            {
                rb.velocity = rb.velocity + Vector2.right * Input.GetAxisRaw("Horizontal") * airAcceleration;
            }
            else if (rb.velocity.x * Input.GetAxisRaw("Horizontal") < 0)
            {
                rb.velocity = rb.velocity + Vector2.right * Input.GetAxisRaw("Horizontal") * airAcceleration;
            }
        }
    }
}

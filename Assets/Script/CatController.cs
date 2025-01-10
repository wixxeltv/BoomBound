using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatMovements : MonoBehaviour
{
    public float catSpeed;
    public float maxSpeed = 10f;
    public float jump = 10f;
    public float slideFactor; 
    public float accelerationFactor;
    private Rigidbody2D rb2d;
    private Animator animator;
    private SpriteRenderer catRenderer;

    private Vector2 respawnPoint;
    public LayerMask groundLayer;
    private float leftRight;
    private AudioSource dieSource;
    void Start()
    {
        dieSource = GetComponent<AudioSource>();
        catRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
    }

    void FixedUpdate()
    {
        Vector2 boxSize = new Vector2(0.5f, 0.1f); 

        animator.SetBool("InAir", !Physics2D.BoxCast(transform.position, boxSize, 0f, Vector2.down, 0.02f, groundLayer));

        if (rb2d.linearVelocity.y > 0)
        {
            animator.SetBool("IsFalling", false);
        }
        else if (rb2d.linearVelocity.y < 0)
        {
            animator.SetBool("IsFalling", true);
        }

        animator.SetFloat("SpeedMultiplier", Mathf.Abs(rb2d.linearVelocity.x / 4));
        animator.SetBool("IsWalking", leftRight != 0);

        if (leftRight > 0 && catRenderer.flipX || leftRight < 0 && !catRenderer.flipX)
        {
            catRenderer.flipX = !catRenderer.flipX;
        }
        float targetSpeed = leftRight * catSpeed;
        float currentSpeed = rb2d.linearVelocity.x;
        
        if (leftRight == 0)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x * (1 - slideFactor), rb2d.linearVelocity.y);
        }
        else
        {
            if ((currentSpeed > 0 && leftRight < 0) || (currentSpeed < 0 && leftRight > 0) || Mathf.Abs(currentSpeed) < maxSpeed)
            {
                float acceleration = accelerationFactor * catSpeed;
                rb2d.linearVelocity = new Vector2(
                    Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration), 
                    rb2d.linearVelocity.y
                );
            }
            else
            {
                rb2d.linearVelocity = new Vector2(
                    Mathf.Sign(currentSpeed) * maxSpeed, 
                    rb2d.linearVelocity.y
                );
            }
        }
    }

    
    void OnLeftRight(InputValue value)
    {
        leftRight = value.Get<float>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && !animator.GetBool("InAir"))
        {
            rb2d.AddRelativeForce(new Vector2(0f, jump), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
         {
             if (collision.CompareTag("Hostile"))
             {
                 Die();
             }
            
         }

    public void ChangeRespawnPoint(Vector2 respawnPoint)
    {
        this.respawnPoint = respawnPoint;
    }
    private void Die()
    {
        dieSource.PlayOneShot(dieSource.clip);
        transform.position = respawnPoint;
        rb2d.linearVelocity = Vector2.zero;
    }
}
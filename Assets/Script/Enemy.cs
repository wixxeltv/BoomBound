using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public LayerMask obstacleLayer;
    private SpriteRenderer enemyRenderer;
    private Rigidbody2D rb2d;
    private bool movingRight;
    public float velocityThreshold = 5f;
    private bool IsDead;
    public float detectionDistance = 0.5f;
    private Animator animator;
    private AudioSource dieSound;
    void Start()
    {
        dieSound = GetComponent<AudioSource>();
        movingRight = true;
        IsDead = false;
        rb2d = GetComponent<Rigidbody2D>();
        enemyRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!IsDead)
        {
            Move();
            DetectObstacle();
        }
        else
        {
            if (!dieSound.isPlaying)
            {
                dieSound.PlayOneShot(dieSound.clip);
            }
            animator.SetTrigger("Die");
            Destroy(gameObject,0.7f);
        }
    }

    void Move()
    {
        float direction = movingRight ? 1 : -1;
        
        rb2d.linearVelocity = new Vector2(direction * speed, rb2d.linearVelocity.y);
    }

    void DetectObstacle()
    {
        Vector2 castOrigin = new Vector2(transform.position.x, transform.position.y);
        Vector2 castDirection = movingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.BoxCast(castOrigin, new Vector2(1f, 1f), 0f, castDirection, detectionDistance, obstacleLayer);
        if (hit.collider != null)
        {
            movingRight = !movingRight;
            enemyRenderer.flipX = !enemyRenderer.flipX;
        }
    }

    private void FixedUpdate()
    {

        if (!IsDead && rb2d.linearVelocity.magnitude > velocityThreshold)
        {
            IsDead = true;
        }
    }

    private void OnDrawGizmos()
    {
        // Dessiner une représentation visuelle du BoxCast dans l'éditeur
        Vector2 castDirection = movingRight ? Vector2.right : Vector2.left;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            transform.position + (Vector3)(castDirection * detectionDistance),
            new Vector3(1f, 1f, 0f)
        );
    }
}

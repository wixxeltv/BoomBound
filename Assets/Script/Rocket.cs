using System;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public GameObject collisionEffect;
    public float explosionRadius;
    public float explosionForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (collisionEffect != null)
        {
            GameObject _exp = Instantiate(collisionEffect, transform.position, Quaternion.identity);
            Destroy(_exp, 3);
        }
        knockBack();
        Destroy(gameObject);
    }

    void knockBack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D nearby in colliders)
        {
            Rigidbody2D rigg = nearby.GetComponent<Rigidbody2D>();
            if (rigg != null)
            {
                
                Vector2 direction = (rigg.transform.position - transform.position).normalized;
                direction += Vector2.up * 1f;
                direction.Normalize();
                
                rigg.AddForce(direction * explosionForce, ForceMode2D.Force);
            }
        }
    }




    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameObject deathEffect;
    public float health = 100f;
    public TextMeshProUGUI healthText;
    public GameObject dieSound;
    public void Start()
    {
        if(healthText == null) return;
        healthText.text = "HP: " + health.ToString("F0");
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        UpdateHealthText();
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }
    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + health.ToString("F0");
        }
    }
    private void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        Instantiate(dieSound, transform.position, Quaternion.identity);

        Destroy(gameObject);

        if (gameObject.CompareTag("Player"))
        {
            //GameManager.StopGame(2);
        }
    }
}
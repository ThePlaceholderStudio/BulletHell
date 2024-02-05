using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public static event Action OnPlayerDeath;

    Player character;

    private void Awake()
    {
        character = GetComponent<Player>();
        maxHealth = character.MaxHp.Value;
        currentHealth = maxHealth;
    }

    void Start()
    {
        
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            OnPlayerDeath?.Invoke();
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
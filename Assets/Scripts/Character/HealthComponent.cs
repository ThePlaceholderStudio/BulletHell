using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public static event Action OnPlayerDeath;

    Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
        maxHealth = character.MaxHp.Value;
    }

    void Start()
    {
        currentHealth = maxHealth;
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
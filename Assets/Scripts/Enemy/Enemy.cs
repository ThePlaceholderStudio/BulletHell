using UnityEngine;

[DisallowMultipleComponent]
public class Enemy : MonoBehaviour
{
    public int experienceAmount = 100;
    public GameObject pfXP;

    public void TakeDamage(float damageAmount)
    {
        HealthComponent health = GetComponent<HealthComponent>();
        health.TakeDamage(damageAmount);

        health.currentHealth -= damageAmount;

        if (health.currentHealth <= 0)
        {
            HandleEnemyDeath();
            health.Die();
        }
    }

    void HandleEnemyDeath()
    {
        //CharacterStats.Instance.killCount++;
        var xp = Instantiate(pfXP, transform.position, Quaternion.identity);
        var xpCollectible = xp.GetComponent<Experience>();
        if (xpCollectible != null)
        {
            xpCollectible.Amount = experienceAmount;
        }
    }
}

using UnityEngine;


[DisallowMultipleComponent]
public class Enemy : MonoBehaviour
{
    public float currentHealth;

    public int ExperienceAmount = 100;
    public float EnemyDamage = 10;

    public GameObject pfXP;
    private Character character;

    private void Awake()
    {
        character = GameManager.Instance.player.GetComponent<Character>();
    }

    void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<Character>();
        if (player != null)
            player.GetComponent<HealthComponent>().TakeDamage(EnemyDamage - character.Armor.Value);
        Debug.Log($"Damage received : {EnemyDamage - character.Armor.Value}");
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            HandleEnemyDeath();
            Die();
        }
    }

    void HandleEnemyDeath()
    {
        //CharacterStats.Instance.killCount++;
        var xp = Instantiate(pfXP, transform.position, Quaternion.identity);
        var xpCollectible = xp.GetComponent<Experience>();
        if (xpCollectible != null)
        {
            xpCollectible.Amount = ExperienceAmount;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}

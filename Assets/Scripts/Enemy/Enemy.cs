using System;
using UnityEngine;


[DisallowMultipleComponent]
public class Enemy : MonoBehaviour
{

    public string EnemyName { get; set; }

    public float currentHealth;

    public int ExperienceAmount = 100;
    public float EnemyDamage = 10;

    public GameObject pfXP;
    protected Character character;

    private void Awake()
    {
        character = GameManager.Instance.player.GetComponent<Character>();

        Init();
    }

    protected virtual void Init() 
    {
        EnemyName = "Minion";
    }

    public Action<Enemy> onEnemyKilled;

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
        onEnemyKilled?.Invoke(this);
        Destroy(gameObject);
    }
}

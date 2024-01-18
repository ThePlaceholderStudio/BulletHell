using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int LifeSpan = 10;
    public int ImpactDamage = 5;

    protected Character character;
    public bool IsFiredFromEnemy = false;
    private void Start()
    {
        character = FindObjectOfType<Character>();
    }

    public void ProjectilePhysics(Vector3 shootDir, int velocity)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(shootDir * velocity, ForceMode.VelocityChange);

        transform.eulerAngles = shootDir;
        Destroy(gameObject, LifeSpan);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!IsFiredFromEnemy && collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(CalculatePlayerDamage());
            Destroy(gameObject);
        }
        else if(IsFiredFromEnemy && collider.gameObject.TryGetComponent(out Character player))
        {
            float damage = CalculateEnemyDamage();
            player.GetComponent<HealthComponent>().TakeDamage(damage);
            Debug.Log($"Damage received : {damage}");
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Damage dealt when the projectile belongs to the enemy
    /// </summary>
    protected float CalculateEnemyDamage()
    {
        return Mathf.Clamp(ImpactDamage - character.Armor.Value, 0, ImpactDamage);
    }

    /// <summary>
    /// Damage dealt when the projectile belongs to the player
    /// </summary>
    /// <returns></returns>
    protected float CalculatePlayerDamage()
    {
        float baseDamage = character.Damage.Value * ImpactDamage;
        float damageToDeal = baseDamage * CalculateCrit();

        if (CalculateCrit() > 1) 
        {
            Debug.Log("Crit: " + CalculateCrit());
        }

        Debug.Log("Damage dealt: " + damageToDeal);
        return damageToDeal;
    }

    private float CalculateCrit()
    {
        if (Random.value <= character.CriticalChance.Value)
        {
            return 1 + character.CriticalDamage.Value;
        }
        return 1;
    }
}
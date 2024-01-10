using UnityEngine;

public class AmmoType : MonoBehaviour
{
    public int LifeSpan = 10;
    public int ImpactDamage = 5;

    Character character;

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
        if (collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(CalculateDamage());
            Destroy(gameObject);
        }
    }

    public float CalculateDamage()
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
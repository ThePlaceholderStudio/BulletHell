using UnityEngine;
using UnityEngine.ProBuilder;

public class PenetratingProjectile : Projectile
{
    public int PenetrationCount = 3; // Number of enemies the bullet can penetrate

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(CalculateDamage(ImpactDamage));
            PenetrationCount--;

            if (PenetrationCount <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

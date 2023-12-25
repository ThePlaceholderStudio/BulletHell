using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Velocity = 10;
    public int LifeSpan = 10;
    public int Damage = 5;

    public void ProjectilePhysics(Vector3 shootDir)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(shootDir * Velocity, ForceMode.VelocityChange);

        transform.eulerAngles = shootDir;
        Destroy(gameObject, LifeSpan);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }

}

using System.Collections;
using UnityEngine;

public class HomingMissile : Projectile
{
    private Vector3 targetPosition;
    private bool hasTarget = false;
    private Rigidbody rb;
    public float moveSpeed = 10;

    public float explosionRadius = 5f;
    public float explosionDamage = 10f;

    public float homingDelay = 0.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Homing());
    }

    private IEnumerator Homing()
    {
        yield return new WaitForSeconds(homingDelay);

        while (true)
        {
            if (hasTarget && targetPosition != null)
            {
                Vector3 targetDirection = (targetPosition - transform.position).normalized;
                Vector3 newVelocity = new Vector3(targetDirection.x, 0, targetDirection.z) * moveSpeed;

                // Gradually change the missile's velocity over time
                rb.velocity = Vector3.Lerp(rb.velocity, newVelocity, Time.fixedDeltaTime);
            }
            else if (targetPosition == null)
            {
                // If the target is missing, stop homing
                hasTarget = false;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            Explode();
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in colliders)
        {
            if (hit.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(explosionDamage);
            }
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
    }
}
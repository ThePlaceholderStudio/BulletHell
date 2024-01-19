using System.Collections;
using UnityEngine;

public class HomingMissile : Projectile
{
    private Vector3 targetPosition;
    private bool hasTarget = false;
    private Rigidbody rb;
    public float moveSpeed = 15;

    public float explosionRadius = 5f;
    public float explosionDamage = 10f;

    public float homingDelay = 0.5f;

    [Header("Test")]
    #region test
    Quaternion targetRotation;

    public Transform target;            // target object 
    public float speed = 0.1F;          // speed scaling factor

    bool rotating = false;              // toggles the rotation, after targeting, toggle true, false after arrives

    float rotationTime; // when rotationTime == 1, will have rotated to our target
    #endregion test

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Homing());
    }

    //private IEnumerator Homing()
    //{
    //    yield return new WaitForSeconds(homingDelay);

    //    while (true)
    //    {
    //        if (hasTarget && targetPosition != null)
    //        {
    //            Vector3 targetDirection = (targetPosition - transform.position).normalized;
    //            Vector3 newVelocity = new Vector3(targetDirection.x, 0, targetDirection.z) * moveSpeed;

    //            // Gradually change the missile's velocity over time
    //            rb.velocity = Vector3.Lerp(rb.velocity, newVelocity, Time.fixedDeltaTime);
    //        }
    //        else if (targetPosition == null)
    //        {
    //            // If the target is missing, stop homing
    //            hasTarget = false;
    //            Destroy(gameObject);
    //        }
    //        yield return new WaitForFixedUpdate();
    //    }
    //}

    //private IEnumerator Homing()
    //{
    //    yield return new WaitForSeconds(homingDelay);

    //    while (true)
    //    {
    //        if (hasTarget && targetPosition != null)
    //        {
    //            Vector3 targetDirection = (targetPosition - transform.position).normalized;
    //            Vector3 newVelocity = new Vector3(targetDirection.x, 0, targetDirection.z) * moveSpeed;

    //            // Gradually change the missile's velocity over time
    //            rb.velocity = Vector3.Lerp(rb.velocity, newVelocity, Time.fixedDeltaTime);

    //            // Rotate the missile towards the target
    //            transform.rotation = Quaternion.LookRotation(targetDirection);
    //        }
    //        else if (targetPosition == null)
    //        {
    //            // If the target is missing, stop homing
    //            hasTarget = false;
    //            Destroy(gameObject);
    //        }
    //        yield return new WaitForFixedUpdate();
    //    }
    //}

    private IEnumerator Homing()
    {
        yield return new WaitForSeconds(homingDelay);
        // Target Phase
        if (hasTarget)
        {
            Vector3 relativePosition = target.position - transform.position;
            targetRotation = Quaternion.LookRotation(relativePosition);
            rotating = true;
            rotationTime = 0;
        }

        // Rotation Phase
        if (rotating)
        {
            rotationTime += Time.deltaTime * speed;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationTime);
            if (rotationTime > 1)
            {
                rotating = false;
            }
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

    public void SetTarget(Transform position)
    {
        target = position;
        //targetPosition = position;
        hasTarget = true;
    }
}
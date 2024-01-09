using System;
using UnityEngine;

public class Experience : MonoBehaviour, ICollectible
{
    public int Amount { get; set; } = 10;

    Rigidbody rb;

    bool hasTarget;
    Vector3 targetPosition;
    float moveSpeed = 20f;

    public static Action<int> OnExperienceCollected;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void FixedUpdate()
    {
        if (hasTarget)
        {
            Vector3 targetDirection = (targetPosition - transform.position).normalized;
            rb.velocity = new Vector3(targetDirection.x, 0, targetDirection.z) * moveSpeed;
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
    }

    public void Collect()
    {
        ExperienceManager.Instance.AddExperience(Amount);
        Destroy(gameObject);
        OnExperienceCollected?.Invoke(Amount);
    }
}

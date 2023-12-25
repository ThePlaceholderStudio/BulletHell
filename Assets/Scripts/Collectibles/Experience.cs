using System;
using UnityEngine;

public class Experience : MonoBehaviour, ICollectible
{
    public int Amount { get; set; } = 10;

    public static Action<int> OnExperienceCollected;

    public void Collect()
    {
        OnExperienceCollected?.Invoke(Amount);
        ExperienceManager.Instance.AddExperience(Amount);
        Destroy(gameObject);
    }
}

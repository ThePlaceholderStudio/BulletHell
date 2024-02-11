using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeRegen : MonoBehaviour
{
    private Player character;
    private HealthComponent healthComponent;

    private void Awake()
    {
        character = GameManager.Instance.player.GetComponent<Player>();
    }

    void Start()
    {
        healthComponent = character.GetComponent<HealthComponent>();
        StartCoroutine(RegenerateHealth());
    }

    IEnumerator RegenerateHealth()
    {
        while (true)
        {
            if (healthComponent.currentHealth < healthComponent.maxHealth)
            {
                healthComponent.Heal(character.LifeRegen.Value);
            }

            yield return new WaitForSeconds(1);
        }
    }
}

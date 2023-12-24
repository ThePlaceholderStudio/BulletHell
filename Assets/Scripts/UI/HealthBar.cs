using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : FillStatusBar
{
    public HealthComponent playerHealth;

    // Update is called once per frame
    void Update()
    {
        UpdateFill(playerHealth.currentHealth, playerHealth.maxHealth);
    }
}

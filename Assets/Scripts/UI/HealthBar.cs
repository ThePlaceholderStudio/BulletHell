public class HealthBar : FillStatusBar
{
    public HealthComponent playerHealth;

    // Update is called once per frame
    void Update()
    {
        UpdateFill(playerHealth.currentHealth, playerHealth.maxHealth);
    }
}

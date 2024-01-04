public class HealthBar : FillStatusBar
{
    public HealthComponent playerHealth;

    private void Start()
    {
        playerHealth = GameManager.Instance.player.GetComponent<HealthComponent>();
    }

    void Update()
    {
        UpdateFill(playerHealth.currentHealth, playerHealth.maxHealth);
    }
}

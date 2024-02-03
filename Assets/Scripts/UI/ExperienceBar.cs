using TMPro;

public class ExperienceBar : FillStatusBar
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI experienceText;
    public Player character;

    private void Start()
    {
        character = GameManager.Instance.player.GetComponent<Player>();
    }

    void Update()
    {
        UpdateFill(character.currentExperience, character.maxExperience);
        UpdateLevelText();
        UpdateExperienceText();
    }

    private void UpdateLevelText()
    {
        levelText.text = "Level: " + character.currentLevel.ToString();
    }

    private void UpdateExperienceText()
    {
        experienceText.text = character.currentExperience.ToString() + "/" + character.maxExperience.ToString();
    }
}

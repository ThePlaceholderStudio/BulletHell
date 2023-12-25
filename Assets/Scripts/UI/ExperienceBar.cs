using TMPro;

public class ExperienceBar : FillStatusBar
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI experienceText;
    public Character character;

    // Update is called once per frame
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

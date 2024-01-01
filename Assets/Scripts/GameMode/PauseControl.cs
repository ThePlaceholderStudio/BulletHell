using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseControl : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject traitsPanel;
    [SerializeField] GameObject gameSummary;

    float previousTimeScale = 1;
    public static bool isPaused;

    public static PauseControl Instance;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            menu.SetActive(isPaused);
        }
    }

    private void OnEnable()
    {
        Character.OnLevelUp += ToggleTraitsPanel;
        Character.OnEquip += ToggleTraitsPanel;
        HealthComponent.OnPlayerDeath += ToggleGameSummary;
    }

    private void OnDisable()
    {
        Character.OnLevelUp -= ToggleTraitsPanel;
        Character.OnEquip -= ToggleTraitsPanel;
        HealthComponent.OnPlayerDeath -= ToggleGameSummary;
    }

    public void ToggleTraitsPanel()
    {
        TogglePause();
        traitsPanel.SetActive(isPaused);
    }

    private void ToggleGameSummary()
    {
        TogglePause();
        gameSummary.SetActive(isPaused);
    }

    public void ResumeGame()
    {
        TogglePause();
        menu.SetActive(isPaused);
    }

    public void OnRetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        TogglePause();
    }

    public void TogglePause()
    {
        if (Time.timeScale > 0)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
            AudioListener.pause = true;

            isPaused = true;
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = previousTimeScale;
            AudioListener.pause = false;

            isPaused = false;
        }
    }
}

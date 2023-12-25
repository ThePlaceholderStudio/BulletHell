using UnityEngine;

public class PauseControl : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject traitsPanel;

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
        Character.OnLevelUp += TogglePause;
        Character.OnLevelUp += ToggleTraitsPanel;
        Character.OnEquip += TogglePause;
        Character.OnEquip += ToggleTraitsPanel;
    }

    private void OnDisable()
    {
        Character.OnLevelUp -= TogglePause;
        Character.OnLevelUp -= ToggleTraitsPanel;
        Character.OnEquip -= TogglePause;
        Character.OnEquip -= ToggleTraitsPanel;
    }

    private void ToggleTraitsPanel()
    {
        traitsPanel.SetActive(isPaused);
    }

    public void ResumeGame()
    {
        TogglePause();
        menu.SetActive(isPaused);
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

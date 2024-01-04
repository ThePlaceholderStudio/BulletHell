using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject traitsPanel;
    [SerializeField] GameObject gameSummary;

    float previousTimeScale = 1;
    public static bool isPaused = false;

    public static global::PanelManager Instance;

    private Panel panelManager;

    private void Awake()
    {
        Instance = this;
        panelManager = new Panel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !panelManager.AnyPanelActive())
        {
            TogglePause();
            menu.SetActive(isPaused);
        }
    }

    private void OnEnable()
    {
        Character.OnLevelUp += ToggleTraitsPanel;
        HealthComponent.OnPlayerDeath += ToggleGameSummary;
    }

    private void OnDisable()
    {
        Character.OnLevelUp -= ToggleTraitsPanel;
        HealthComponent.OnPlayerDeath -= ToggleGameSummary;
    }

    public void ToggleTraitsPanel()
    {
        TogglePanel(traitsPanel);
    }

    private void ToggleGameSummary()
    {
        TogglePanel(gameSummary);
    }

    public void OnResumeButton()
    {
        TogglePanel(menu);
    }

    public void OnRetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        TogglePause();
    }

    public void TogglePanel(GameObject panel)
    {
        TogglePause();
        panel.SetActive(isPaused);
        if (panel.activeSelf)
            panelManager.RegisterPanel(panel);
        else
            panelManager.UnregisterPanel(panel);
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

    public class Panel
    {
        private HashSet<GameObject> activePanels = new HashSet<GameObject>();

        public void RegisterPanel(GameObject panel)
        {
            activePanels.Add(panel);
        }

        public void UnregisterPanel(GameObject panel)
        {
            activePanels.Remove(panel);
        }

        public bool AnyPanelActive()
        {
            return activePanels.Count > 0;
        }
    }
}

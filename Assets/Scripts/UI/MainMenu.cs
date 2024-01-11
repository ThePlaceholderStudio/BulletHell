using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.menuStack.Push(gameObject);
    }

    public void OnPlayButton()
    {
        UIManager.Instance.SwitchMenus(UIManager.Instance.levelSelection.gameObject);
    }
    public void OnSettingsButton()
    {
        UIManager.Instance.SwitchMenus(UIManager.Instance.settingsMenu.gameObject);
    }
    public void OnQuit()
    {
        Application.Quit();
    }
}

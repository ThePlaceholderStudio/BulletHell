using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public MainMenu mainMenu;
    public SettingsMenu settingsMenu;
    public LevelSelection levelSelection;
    public CharacterSelection characterSelection;

    [System.NonSerialized]
    public int LevelIndex;

    public static UIManager Instance { get; private set; }

    public Stack<GameObject> menuStack = new Stack<GameObject>();

    private void Awake() => Instance = this;

    public void SwitchMenus(GameObject next)
    {
        if (menuStack.Count > 0)
        {
            GameObject currentMenu = menuStack.Peek();
            currentMenu.SetActive(false);
        }
        next.SetActive(true);
        menuStack.Push(next);
    }

    public void GoBack()
    {
        if (menuStack.Count > 0)
        {
            GameObject currentMenu = menuStack.Pop();
            currentMenu.SetActive(false);
        }

        if (menuStack.Count > 0)
        {
            GameObject previousMenu = menuStack.Peek();
            previousMenu.SetActive(true);
        }
    }
}


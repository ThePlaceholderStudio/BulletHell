using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    public void OnSelectButton(int index)
    {
        UIManager.Instance.SwitchMenus(UIManager.Instance.characterSelection.gameObject);
        UIManager.Instance.LevelIndex = index;
    }

    public void OnBackButton()
    {
        UIManager.Instance.GoBack();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public Transform CharacterContainer;

    private List<GameObject> characters;

    private int selectionIndex = 0;

    private void Start()
    {
        characters = new List<GameObject>();
        foreach (Transform t in CharacterContainer.transform)
        {
            characters.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }

        characters[selectionIndex].SetActive(true);
    }

    public void Select(int index)
    {
        if (index == selectionIndex)
            SelectionManager.Instance.PlayerPrefab = characters[selectionIndex];

        if (index < 0 || index > characters.Count)
            return;

        characters[selectionIndex].SetActive(false);
        selectionIndex = index;
        characters[selectionIndex].SetActive(true);

        SelectionManager.Instance.PlayerPrefab = characters[selectionIndex];

        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Unload the previous Scene
        yield return SceneManager.UnloadSceneAsync(currentScene);

        // Set the new scene as the active scene
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
    }

    public void OnBackButton()
    {
        UIManager.Instance.GoBack();
    }
}

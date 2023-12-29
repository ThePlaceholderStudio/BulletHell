using UnityEngine;

public class ActivationSystem : MonoBehaviour
{
    public GameObject[] Items; // The array that holds all the items that the player has
    public int StartingItemIndex = 0; // The item index that the player will start with
    public int ItemIndex; // The current index of the active item

    void Start()
    {
        ItemIndex = StartingItemIndex;
    }

    public void SetActiveItem(int index)
    {
        // Make sure this item exists before trying to switch to it
        if (index >= Items.Length || index < 0)
        {
            Debug.LogWarning("Tried to switch to an item that does not exist.  Make sure you have all the correct items in your items array.");
            return;
        }

        // Send a messsage so that users can do other actions whenever this happens
        SendMessageUpwards("OnEasySwitch", SendMessageOptions.DontRequireReceiver);

        // Make sure the ItemIndex references the correct item
        ItemIndex = index;

        // Activate the one item that we want
        Items[index].SetActive(true);
    }
}
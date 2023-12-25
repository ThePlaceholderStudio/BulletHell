using UnityEngine;
using System.Collections;

public class WeaponSystem : MonoBehaviour
{
    public static WeaponSystem Instance;

    public GameObject[] Weapons;                // The array that holds all the weapons that the player has
    public int StartingWeaponIndex = 0;         // The weapon index that the player will start with
    public int WeaponIndex;                    // The current index of the active weapon


    // Use this for initialization
    void Start()
    {
        // Make sure the starting active weapon is the one selected by the user in startingWeaponIndex
        WeaponIndex = StartingWeaponIndex;
        SetActiveWeapon(WeaponIndex);
    }

    public void SetActiveWeapon(int index)
    {
        // Make sure this weapon exists before trying to switch to it
        if (index >= Weapons.Length || index < 0)
        {
            Debug.LogWarning("Tried to switch to a weapon that does not exist.  Make sure you have all the correct weapons in your weapons array.");
            return;
        }

        // Send a messsage so that users can do other actions whenever this happens
        SendMessageUpwards("OnEasyWeaponsSwitch", SendMessageOptions.DontRequireReceiver);

        // Make sure the weaponIndex references the correct weapon
        WeaponIndex = index;

        // Activate the one weapon that we want
        Weapons[index].SetActive(true);
    }

    public void NextWeapon()
    {
        WeaponIndex++;
        if (WeaponIndex > Weapons.Length - 1)
            WeaponIndex = 0;
        SetActiveWeapon(WeaponIndex);
    }

    public void PreviousWeapon()
    {
        WeaponIndex--;
        if (WeaponIndex < 0)
            WeaponIndex = Weapons.Length - 1;
        SetActiveWeapon(WeaponIndex);
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class CharacterDescription : MonoBehaviour
{
    public static CharacterDescription Instance;

    [SerializeField] Text nameText;
    [SerializeField] Text statsText;
    [SerializeField] Image weaponIcon;

    private StringBuilder sb = new StringBuilder();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        weaponIcon.enabled = true;
    }

    public void ShowTooltip(Player charactertoShow)
    {
        if (charactertoShow == null)
        {
            return;
        }

        weaponIcon.sprite = charactertoShow.DefaultWeapon.Icon;

        Player character = charactertoShow;

        gameObject.SetActive(true);

        nameText.text = character.name;

        sb.Length = 0;

        AddStatText(character.MaxHp.Value, " MaxHp");
        AddStatText(character.LifeRegen.Value, " Life Regen");
        AddStatText(character.Armor.Value, " Armor");
        AddStatText(character.DashCoolDown.Value, " Dash Cool-Down");
        AddStatText(character.DashRange.Value, " Dash Range");
        AddStatText(character.MoveSpeed.Value, " Move Speed");
        AddStatText(character.Damage.Value, " Damage");
        AddStatText(character.FireRate.Value, " Fire Rate");
        AddStatText(character.ReloadSpeed.Value, " Reload Speed");
        AddStatText(character.CriticalChance.Value, " Critical Chance");
        AddStatText(character.CriticalDamage.Value, " Critical Damage");
        AddStatText(character.PickUpRadius.Value, " Pick Up Radius");
        AddStatText(character.XPGain.Value, " XP Gain");

        statsText.text = sb.ToString();
    }


    private void AddStatText(float statBonus, string statName)
    {
        if (statBonus != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (statBonus > 0)
                sb.Append("+");

            sb.Append(statBonus);
            sb.Append(statName);
        }
    }
}

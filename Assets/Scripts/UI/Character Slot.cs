using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using UnityEngine.EventSystems;

public class CharacterSlot : MonoBehaviour , IPointerEnterHandler
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI statsText;

    public event Action<Character> OnButtonClickEvent;

    private StringBuilder sb = new StringBuilder();

    private Character _character;
    public Character Character
    {
        get { return _character; }
        set
        {
            _character = value;

            if (_character != null)
            {

                Character character = _character;

                gameObject.SetActive(true);

                if (_character != null && nameText != null)
                {
                    nameText.text = _character.name;
                }

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

                if (_character != null && statsText != null)
                    statsText.text = sb.ToString();
            }
        }
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

    private void Start()
    {
        CharacterDescription.Instance.ShowTooltip(Character);
    }

    protected virtual void OnValidate()
    {
        if (nameText == null)
            nameText = GetComponent<TextMeshProUGUI>();

        if (statsText == null)
            statsText = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CharacterSelection.Instance.selectionIndex != transform.GetSiblingIndex())
        {
            CharacterSelection.Instance.characters[transform.GetSiblingIndex()].SetActive(true);
            CharacterSelection.Instance.characters[CharacterSelection.Instance.selectionIndex].SetActive(false);
            CharacterSelection.Instance.selectionIndex = transform.GetSiblingIndex();
        }
        _character = CharacterSelection.Instance.characters[transform.GetSiblingIndex()].GetComponent<Character>();
        CharacterDescription.Instance.ShowTooltip(Character);
    }
}

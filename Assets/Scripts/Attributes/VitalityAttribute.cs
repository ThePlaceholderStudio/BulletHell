#if UNITY_EDITOR
using UnityEditor;
#endif

public class VitalityAttribute : Attribute
{
    public float MaxHp;
    public float LifeRegen;
    public float Armor;

#if UNITY_EDITOR
    public override void DoLayout()
    {
        MaxHp = EditorGUILayout.FloatField("Max HP", MaxHp);
        LifeRegen = EditorGUILayout.FloatField("Life Regen", LifeRegen);
        Armor = EditorGUILayout.FloatField("Armor", Armor);
    }
#endif
}

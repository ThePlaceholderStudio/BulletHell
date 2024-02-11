#if UNITY_EDITOR
using UnityEditor;
#endif

public class CombatAttribute : Attribute
{

    public float Damage;
    public float FireRate;
    public float ReloadSpeed;
    public float CriticalChance;
    public float CriticalDamage;


#if UNITY_EDITOR
    public override void DoLayout()
    {
        Damage = EditorGUILayout.FloatField("Damage", Damage);
        FireRate = EditorGUILayout.FloatField("Fire Rate", FireRate);
        ReloadSpeed = EditorGUILayout.FloatField("Reload Speed", ReloadSpeed);
        CriticalChance = EditorGUILayout.FloatField("Critical Chance", CriticalChance);
        CriticalDamage = EditorGUILayout.FloatField("Critical Damage", CriticalDamage);
    }
#endif
}

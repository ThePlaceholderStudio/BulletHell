#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

public class ArsenalAttribute : Attribute
{
    public ScriptableObject obj;

#if UNITY_EDITOR
    public override void DoLayout()
    {
        obj = EditorGUILayout.ObjectField("Weapon", obj, typeof(ScriptableObject), false) as ScriptableObject;
    }
#endif
}

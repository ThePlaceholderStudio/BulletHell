#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

public class IdentityAttribute : Attribute
{
    public GameObject prefab;

#if UNITY_EDITOR
    public override void DoLayout()
    {
        prefab = EditorGUILayout.ObjectField("Character Prefab", prefab, typeof(GameObject), false) as GameObject;
    }
#endif
}

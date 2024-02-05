#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using static Unity.VisualScripting.Member;
#endif

public class IdentityAttribute : Attribute
{
    public GameObject source;

#if UNITY_EDITOR
    public override void DoLayout()
    {
        source = EditorGUILayout.ObjectField("Prefab", source, typeof(GameObject), false) as GameObject;
    }
#endif
}

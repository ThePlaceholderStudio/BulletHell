using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Attribute : ScriptableObject
{

#if UNITY_EDITOR
    public virtual void DoLayout() { }
#endif

}

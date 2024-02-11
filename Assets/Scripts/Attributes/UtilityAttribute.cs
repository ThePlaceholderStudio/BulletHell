#if UNITY_EDITOR
using UnityEditor;
#endif

public class UtilityAttribute : Attribute
{

    public float PickUpRadius;
    public float XPGain;


#if UNITY_EDITOR
    public override void DoLayout()
    {
        PickUpRadius = EditorGUILayout.FloatField("Pick-Up Radius", PickUpRadius);
        XPGain = EditorGUILayout.FloatField("XP Gain", XPGain);
    }
#endif
}

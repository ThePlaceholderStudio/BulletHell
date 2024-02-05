#if UNITY_EDITOR
using UnityEditor;
#endif

public class MovementAttribute : Attribute
{

    public float DashCoolDown;
    public float DashRange;
    public float MoveSpeed;


#if UNITY_EDITOR
    public override void DoLayout()
    {
        DashCoolDown = EditorGUILayout.FloatField("Dash Cool-Down", DashCoolDown);
        DashRange = EditorGUILayout.FloatField("Dash Range", DashRange);
        MoveSpeed = EditorGUILayout.FloatField("Move Speed", MoveSpeed);
    }
#endif
}

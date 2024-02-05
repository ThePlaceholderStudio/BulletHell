using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;

[CustomEditor(typeof(Character))]
public class CharacterEditor : Editor
{

    // Add a menu item to create Item ScriptableObjects:
    [MenuItem("Assets/Create/Character")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<Character>();
    }

    // Holds ItemAttribute types for popup:
    private string[] m_attributeTypeNames = new string[0];
    private int m_attributeTypeIndex = -1;

    private void OnEnable()
    {
        // Fill the popup list:
        Type[] types = Assembly.GetAssembly(typeof(Attribute)).GetTypes();
        m_attributeTypeNames = (from Type type in types where type.IsSubclassOf(typeof(Attribute)) select type.FullName).ToArray();
    }

    public override void OnInspectorGUI()
    {
        var character = target as Character;

        // Draw attributes with a delete button below each one:
        int indexToDelete = -1;
        for (int i = 0; i < character.attributes.Count; i++)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
			if (character.attributes[i] != null) character.attributes[i].DoLayout();
            if (GUILayout.Button("Delete")) indexToDelete = i;
            EditorGUILayout.EndVertical();
        }
        if (indexToDelete > -1) character.attributes.RemoveAt(indexToDelete);

        // Draw a popup and button to add a new attribute:
        EditorGUILayout.BeginHorizontal();
        m_attributeTypeIndex = EditorGUILayout.Popup(m_attributeTypeIndex, m_attributeTypeNames);
        if (GUILayout.Button("Add"))
        {
            // A little tricky because we need to record it in the asset database, too:
            var newAttribute = CreateInstance(m_attributeTypeNames[m_attributeTypeIndex]) as Attribute;
            newAttribute.hideFlags = HideFlags.HideInHierarchy;
            AssetDatabase.AddObjectToAsset(newAttribute, character);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newAttribute));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            character.attributes.Add(newAttribute);
        }
        EditorGUILayout.EndHorizontal();

        if (GUI.changed) EditorUtility.SetDirty(character);
    }

}


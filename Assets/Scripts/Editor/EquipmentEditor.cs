using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Equipement))]
public class EquipementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Equipement equipement = (Equipement)target;
        if (GUILayout.Button("Generate Random Values"))
        {
            equipement.GenerateRandomValues();
            EditorUtility.SetDirty(equipement);
        }
    }
}
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(AutoLevelGenerator))]
public class AutoLevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AutoLevelGenerator myScript = (AutoLevelGenerator)target;
        if (GUILayout.Button("Generate New Level"))
        {
            myScript.Generate();
        }

        if (GUILayout.Button("Clear Current Level"))
        {
            myScript.ResetObjects();
        }

        if (GUILayout.Button("Save Level"))
        {
            myScript.SaveLevel();
        }

        if (GUILayout.Button("Load Level"))
        {
            myScript.LoadLevel();
        }

        if (GUILayout.Button("Update Levels List"))
        {
            myScript.GetLevelList();
        }

        if (GUILayout.Button("Delete Levels"))
        {
            myScript.DeleteLevels();
        }
    }
}

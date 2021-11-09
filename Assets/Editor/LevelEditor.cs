using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelController))]
public class LevelEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LevelController level = (LevelController) target; 

        if (GUILayout.Button("Generate walls"))
        {
            level.GenerateWalls();
        }
        
        if (GUILayout.Button("Clear walls"))
        {
            level.ClearWalls();
        }
    }
}

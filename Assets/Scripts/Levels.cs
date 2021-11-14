using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "NewLevelsSetup", menuName = "Levels setup")]
public class Levels : ScriptableObject
{
    public List<SceneAsset> levels;

    public int GetSceneIdByName(string sceneName)
    {
        SceneAsset found = levels.Find(scene => scene.name == sceneName);
        return levels.IndexOf(found);
    }

    public SceneAsset GetScene(int index)
    {
        if (index > levels.Count - 1)
        {
            Debug.Log($"Scene with index {index} does not exist. Current scenes count: {levels.Count}");
            return null;
        }
        return levels[index];
    }
}

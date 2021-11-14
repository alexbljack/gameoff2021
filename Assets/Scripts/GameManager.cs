using System;
using System.Collections;
using Lib;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    [SerializeField] float debugModeCooldown;
    
    public static event Action EnableDebugModeEvent;
    public static event Action DisableDebugModeEvent;

    public static event Action FinishLevel;

    bool _debugMode;
    
    public void SwitchToDebugMode()
    {
        if (!_debugMode)
        {
            EnableDebugModeEvent?.Invoke();
            StartCoroutine(DebugModeRoutine());
        }
    }

    IEnumerator DebugModeRoutine()
    {
        _debugMode = true;
        yield return new WaitForSeconds(debugModeCooldown);
        _debugMode = false;
        DisableDebugModeEvent?.Invoke();
    }

    public void ExitLevel()
    {
        var setup = Resources.Load<Levels>("LevelsSetup");
        var currentSceneIndex = setup.GetSceneIdByName(SceneManager.GetActiveScene().name);
        SceneAsset nextScene = setup.GetScene(currentSceneIndex + 1);
        if (nextScene != null)
        {
            SceneManager.LoadScene(nextScene.name);
        }
    }
}
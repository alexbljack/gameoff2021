using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lib;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    [SerializeField] float debugModeCooldown;
    
    public static event Action EnableDebugModeEvent;
    public static event Action DisableDebugModeEvent;
    
    bool _debugMode;
    IEnumerator _debugRoutine;
    LevelExit _exit;

    public List<Enemy> Enemies => FindObjectsOfType<Enemy>().ToList();
    public Transform Player => FindObjectOfType<PlayerController>().transform;
    
    void Start()
    {
        _exit = FindObjectOfType<LevelExit>();
        _exit.gameObject.SetActive(false);
    }

    void Update()
    {
        CheckForExitSpawn();
    }

    public void SwitchToDebugMode()
    {
        if (!_debugMode)
        {
            EnableDebugModeEvent?.Invoke();
            StartCoroutine(DebugModeRoutine(debugModeCooldown));
        }
    }

    IEnumerator DebugModeRoutine(float cooldown)
    {
        _debugMode = true;
        yield return new WaitForSeconds(cooldown);
        _debugMode = false;
        DisableDebugModeEvent?.Invoke();
    }

    void CheckForExitSpawn()
    {
        if (Enemies.Count == 0)
        {
            _exit.gameObject.SetActive(true);
        }
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
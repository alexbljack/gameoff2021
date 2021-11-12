using System;
using System.Collections;
using Lib;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    [SerializeField] float debugModeCooldown;
    
    public event Action EnableDebugMode;
    public event Action DisableDebugMode;

    bool _debugMode;
    
    public void SwitchToDebugMode()
    {
        if (!_debugMode)
        {
            EnableDebugMode?.Invoke();
            StartCoroutine(DebugModeRoutine());
        }
    }

    IEnumerator DebugModeRoutine()
    {
        _debugMode = true;
        yield return new WaitForSeconds(debugModeCooldown);
        _debugMode = false;
        DisableDebugMode?.Invoke();
    }
}
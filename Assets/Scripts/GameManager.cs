using System;
using System.Collections;
using Lib;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    [SerializeField] float debugModeCooldown;
    
    public static event Action EnableDebugModeEvent;
    public static event Action DisableDebugModeEvent;

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
}
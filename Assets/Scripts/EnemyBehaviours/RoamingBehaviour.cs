using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


enum RoamingState
{
    Walking,
    Angry,
}


public class RoamingBehaviour : MonoBehaviour
{
    [SerializeField] float detectRadius;
    [SerializeField] float moveSpeed;
    [SerializeField] float roamTime;
    [SerializeField] float waitTime;

    RigidBodyMove _moveController;
    List<EnemyShootBehaviour> _gunBehaviours;
    bool _shouldChangeDir = true;
    float _xInput = 1;
    bool _calming = false;
    RoamingState _state;

    void Awake()
    {
        _moveController = GetComponent<RigidBodyMove>();
        _gunBehaviours = GetComponents<EnemyShootBehaviour>().ToList();
    }

    void Start()
    {
        CalmDown();
    }

    void Update()
    {
        switch (_state)
        {
            case RoamingState.Walking:
                if (PlayerInRange())
                {
                    TriggerAttack();
                }
                if (_shouldChangeDir)
                {
                    _shouldChangeDir = false;
                    StartCoroutine(MoveRoutine());
                }
                break;
            case RoamingState.Angry:
                if (!PlayerInRange() && !_calming)
                {
                    StartCoroutine(CalmingDown());
                }
                break;
        }
    }

    void TriggerAttack()
    {
        _state = RoamingState.Angry;
        SetComponentsState(_gunBehaviours, true);
    }

    void CalmDown()
    {
        _state = RoamingState.Walking;
        SetComponentsState(_gunBehaviours, false);
    }

    IEnumerator CalmingDown()
    {
        _calming = true;
        yield return new WaitForSeconds(1);
        _calming = false;
        if (!PlayerInRange())
        {
            CalmDown();
        }
    }

    void SetComponentsState(List<EnemyShootBehaviour> behaviours, bool enabled)
    {
        foreach (EnemyShootBehaviour behaviour in behaviours)
        {
            behaviour.enabled = enabled;
        }
    }

    bool PlayerInRange()
    {
        return (transform.position - GameManager.Instance.Player.transform.position).magnitude < detectRadius;
    }

    void OnDrawGizmosSelected()
    {
        Utils.GizmosDrawCircle(transform.position, detectRadius, 60, Color.yellow);
    }

    IEnumerator MoveRoutine()
    {
        float timer = 0;
        
        while (timer < roamTime)
        {
            if (_state == RoamingState.Angry)
            {
                break;
            }
            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
            _moveController.Move(_xInput, moveSpeed);
        }
        
        yield return new WaitForSeconds(waitTime);
        _xInput = -_xInput;
        _shouldChangeDir = true;
    }
}
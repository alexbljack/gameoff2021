using System;
using System.Collections;
using UnityEngine;


public class RoamingBehaviour : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float roamTime;
    [SerializeField] float waitTime;
    
    bool _canMove = false;
    float _xInput = 1;

    bool _shouldChangeDir = true;
    
    RigidBodyMove _moveController;

    void Awake()
    {
        _moveController = GetComponent<RigidBodyMove>();
    }

    void Update()
    {
        if (_shouldChangeDir)
        {
            _shouldChangeDir = false;
            StartCoroutine(MoveRoutine());
        }
    }

    void FixedUpdate()
    {
        if (_canMove)
        {
            _moveController.Move(_xInput, moveSpeed);
        }
    }

    IEnumerator MoveRoutine()
    {
        _canMove = true;
        float timer = 0;
        
        while (timer < roamTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        
        _canMove = false;
        yield return new WaitForSeconds(waitTime);
        _xInput = -_xInput;
        _canMove = true;
        _shouldChangeDir = true;
    }
}
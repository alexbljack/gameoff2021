using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAtPlayer : EnemyBehaviour
{
    [SerializeField] float chargeForce;
    [SerializeField] float cooldown;
    
    Rigidbody2D _rb;
    IEnumerator _routine;
    
    bool _canCharge = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    void OnEnable()
    {
        _canCharge = true;
    }

    void OnDisable()
    {
        StopCoroutine(nameof(ChargeAttackRoutine));
        _canCharge = true;
    }

    void Update()
    {
        if (_canCharge)
        {
            StartCoroutine(ChargeAttackRoutine());
        }
    }

    IEnumerator ChargeAttackRoutine()
    {
        _canCharge = false;
        Charge();
        yield return new WaitForSeconds(cooldown);
        _canCharge = true;
    }

    void Charge()
    {
        Vector3 direction = GameManager.Instance.Player.transform.position - transform.position;
        _rb.AddForce(direction.normalized * chargeForce, ForceMode2D.Impulse);
    }
}

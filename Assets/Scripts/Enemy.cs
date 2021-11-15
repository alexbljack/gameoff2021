using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Gun _gun;
    Transform _player;
    Entity _entity;

    void Awake()
    {
        _entity = GetComponent<Entity>();
    }

    void Start()
    {
        _gun = transform.GetComponentInChildren<Gun>();
        _player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 direction = _player.position - transform.position;
        _gun.Shoot(direction);
    }

    void OnEnable()
    {
        _entity.DeadEvent += Die;
    }

    void OnDisable()
    {
        _entity.DeadEvent -= Die;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Entity _entity;

    void Awake()
    {
        _entity = GetComponent<Entity>();
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

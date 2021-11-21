using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    CamShake camShake;
    Entity _entity;
    [SerializeField]
    GameObject dieEffect;
    [SerializeField]
    GameObject bloodSplash;

    void Awake()
    {
        _entity = GetComponent<Entity>();
        camShake = Camera.main.GetComponent<CamShake>();
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
        camShake.Shake();
        Instantiate(bloodSplash, new Vector3(transform.position.x, transform.position.y - 1f), Quaternion.identity);
        Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

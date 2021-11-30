using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    CamShake camShake;
    Entity _entity;
    SpriteRenderer _renderer;
    
    [SerializeField]
    GameObject dieEffect;
    [SerializeField]
    GameObject bloodSplash;

    void Awake()
    {
        _entity = GetComponent<Entity>();
        _renderer = GetComponent<SpriteRenderer>();
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
        Instantiate(bloodSplash, new Vector3(transform.position.x, transform.position.y - 0.75f), Quaternion.identity);
        Instantiate(dieEffect, transform.position, Quaternion.identity);
        _renderer.enabled = false;
        transform.Find("Canvas").gameObject.SetActive(false);
        Destroy(gameObject, _entity.DeathClipLength);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Gun _gun;
    Transform _player;
    
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
}

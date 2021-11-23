using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject effect;

    float _speed = -1;
    Vector2 _direction = Vector2.zero;

    public void Init(float speed, Vector2 direction)
    {
        _speed = speed;
        _direction = direction.normalized * 100;
    }

    void Update()
    {
        if (_speed > 0)
        {
            transform.position += new Vector3(_direction.x, _direction.y, 0).normalized * _speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

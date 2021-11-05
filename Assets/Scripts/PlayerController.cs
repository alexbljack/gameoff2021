using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce;
    
    Rigidbody2D _rb;
    SpriteRenderer _renderer;
    
    float _moveInput;
    bool _pressedJump;
    
    Vector2 _currentVel;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        
        _moveInput = 0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _pressedJump = true;
        }
        
        _moveInput = Input.GetAxisRaw("Horizontal");
        Flip(_moveInput);
    }

    void Move(float input)
    {
        var targetVel = new Vector2(input * runSpeed, _rb.velocity.y);
        _rb.velocity = Vector2.SmoothDamp(_rb.velocity, targetVel, ref _currentVel, 0.05f);
    }

    void FixedUpdate()
    {
        Move(_moveInput);
        
        if (_pressedJump)
        {
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            _pressedJump = false;
        }

        // _rb.velocity = new Vector2(Mathf.Clamp(_rb.velocity.x, -runSpeed, runSpeed), _rb.velocity.y);
    }

    void Flip(float input)
    {
        Vector3 scale = transform.localScale;
        var xScale = Math.Abs(input) > 0 ? Math.Abs(scale.x) * Mathf.Sign(input) : scale.x;
        transform.localScale = new Vector3(xScale, scale.y, scale.z);
    }
}

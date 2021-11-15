using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce;
    
    SpriteRenderer _renderer;
    RigidBodyMove _moveController;

    Gun _gun;

    float _moveInput;
    bool _pressedJump;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _moveController = GetComponent<RigidBodyMove>();
        
        _gun = transform.GetComponentInChildren<Gun>();
    }

    void Start()
    {
        _moveInput = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _pressedJump = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _gun.Shoot(_gun.transform.right);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.Instance.SwitchToDebugMode();
        }
        
        PointGunToMouse();

        _moveInput = Input.GetAxisRaw("Horizontal");
        FlipX();
    }

    void PointGunToMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(_gun.transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
 
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        _gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void FlipX()
    {
        _renderer.flipX = (transform.position.x - Utils.MousePosition().x) > 0;
    }

    void FixedUpdate()
    {
        _moveController.Move(_moveInput, runSpeed);
        
        if (_pressedJump && _moveController.IsOnGround())
        {
            _moveController.Jump(jumpForce);
            _pressedJump = false;
        }

        // _rb.velocity = new Vector2(Mathf.Clamp(_rb.velocity.x, -runSpeed, runSpeed), _rb.velocity.y);
    }
}

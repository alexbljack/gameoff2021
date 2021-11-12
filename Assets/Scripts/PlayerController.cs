using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask groundLayer;
    
    Rigidbody2D _rb;
    SpriteRenderer _renderer;
    Collider2D _collider;

    Gun _gun;

    float _moveInput;
    bool _pressedJump;
    
    Vector2 _currentVel;

    Bounds CBounds => _collider.bounds;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        
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
        // Flip(_moveInput);
        FlipX();
    }

    void PointGunToMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint (_gun.transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
 
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        _gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void FlipX()
    {
        _renderer.flipX = (transform.position.x - MousePosition().x) > 0;
    }

    Vector2 MousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Move(float input)
    {
        Vector2 velocity = _rb.velocity;
        var targetVel = new Vector2(input * runSpeed, velocity.y);

        if (WallTest(new Vector2(input, 0)))
        {
            _rb.velocity = new Vector2(0, velocity.y);
        }
        else
        {
            _rb.velocity = Vector2.SmoothDamp(velocity, targetVel, ref _currentVel, 0.05f);
        }
    }

    void FixedUpdate()
    {
        Move(_moveInput);
        
        if (_pressedJump && IsOnGround())
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

    bool IsOnGround()
    {
        
        RaycastHit2D hit = Physics2D.BoxCast(CBounds.center, CBounds.size, 0f, 
            Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }

    bool WallTest(Vector2 direction)
    {
        Vector2 boxSize = new Vector2(CBounds.size.x, CBounds.size.y * 0.7f);
        RaycastHit2D hit = Physics2D.BoxCast(CBounds.center, boxSize, 0f, 
            direction, 0.1f, groundLayer);
        return hit.collider != null;
    }
}

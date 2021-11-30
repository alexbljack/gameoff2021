using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] GameObject dieEffect;
    [SerializeField] GameObject bloodSplash;

    RigidBodyMove _moveController;
    Rigidbody2D _rb;
    Entity _entity;
    Animator _animator;
    SpriteRenderer _renderer;

    Gun _gun;

    bool _pressedJump;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _entity = GetComponent<Entity>();
        _moveController = GetComponent<RigidBodyMove>();
        _renderer = GetComponent<SpriteRenderer>();
        
        _gun = transform.GetComponentInChildren<Gun>();
        _animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        _entity.DeadEvent += Die;
    }

    void OnDisable()
    {
        _entity.DeadEvent -= Die;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MakeJumpReady();;
        }

        if (Input.GetMouseButtonDown(0))
        {
            ShootGun();
        }
    }

    float GetInputLag()
    {
        var lagsComponents = FindObjectsOfType<InputLagDebuff>();
        var lags = from component in lagsComponents select component.lagInSeconds;
        return lags.Count() > 0 ? Mathf.Max(lags.ToArray()) : 0;
    }

    void ShootGun()
    {
        _gun.Shoot(_gun.transform.right);
    }

    void MakeJumpReady()
    {
        _pressedJump = true;
    }

    void FixedUpdate()
    {
        var moveInput = Input.GetAxis("Horizontal");
        _moveController.Move(moveInput, runSpeed);
        _animator.SetFloat("speed", Mathf.Abs(_rb.velocity.x));

        if (_pressedJump && _moveController.IsOnGround())
        {
            _moveController.Jump(jumpForce);
            _pressedJump = false;
            _animator.SetTrigger("jump");
        }

        _animator.SetBool("isJumping", !_moveController.IsOnGround());
        // _rb.velocity = new Vector2(Mathf.Clamp(_rb.velocity.x, -runSpeed, runSpeed), _rb.velocity.y);
    }

    void Die()
    {
        Instantiate(bloodSplash, new Vector3(transform.position.x, transform.position.y - 0.5f), Quaternion.identity);
        Instantiate(dieEffect, transform.position, Quaternion.identity);
        Hide();
        StartCoroutine(GameManager.Instance.RestartLevel());
    }

    void Hide()
    {
        _renderer.enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}

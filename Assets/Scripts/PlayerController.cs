using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce;
    
    RigidBodyMove _moveController;
    Entity _entity;

    Gun _gun;

    float _moveInput;
    bool _pressedJump;
    
    void Awake()
    {
        _entity = GetComponent<Entity>();
        _moveController = GetComponent<RigidBodyMove>();
        
        _gun = transform.GetComponentInChildren<Gun>();
    }

    void OnEnable()
    {
        _entity.DeadEvent += Die;
    }

    void OnDisable()
    {
        _entity.DeadEvent -= Die;
    }

    void Start()
    {
        _moveInput = 0;
    }

    void Update()
    {
        float lag = GetInputLag();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke(nameof(MakeJumpReady), lag);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Invoke(nameof(ShootGun), lag);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Invoke(nameof(MoveLeft), lag);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Invoke(nameof(MoveRight), lag);
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

    void MoveRight()
    {
        _moveInput = 1;
    }

    void MoveLeft()
    {
        _moveInput = -1;
    }

    void FixedUpdate()
    {
        if (_moveInput != 0f)
        {
            _moveController.Move(_moveInput, runSpeed);
            _moveInput = 0;
        }

        if (_pressedJump && _moveController.IsOnGround())
        {
            _moveController.Jump(jumpForce);
            _pressedJump = false;
        }

        // _rb.velocity = new Vector2(Mathf.Clamp(_rb.velocity.x, -runSpeed, runSpeed), _rb.velocity.y);
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

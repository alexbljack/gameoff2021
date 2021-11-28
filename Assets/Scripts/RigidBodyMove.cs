using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class RigidBodyMove : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    
    public Rigidbody2D _rb;
    Collider2D _collider;

    Vector2 _currentVel;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    public void Move(float input, float moveSpeed)
    {
        Vector2 velocity = _rb.velocity;
        var targetVel = new Vector2(input * moveSpeed, velocity.y);
        
        _rb.velocity = IsNextToWall(input) ? 
            new Vector2(0, velocity.y) :
            Vector2.SmoothDamp(velocity, targetVel, ref _currentVel, 0.05f);
    }

    public void Jump(float jumpForce)
    {
        _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
    
    public bool IsOnGround()
    {
        return Utils.RaycastBox(Vector2.down, _collider.bounds, groundLayer);
    }

    public bool IsNextToWall(float direction)
    {
        var raycastDir = new Vector2(direction, 0);
        return Utils.RaycastBox(raycastDir, _collider.bounds, groundLayer, scaleY: 0.7f);
    }
}
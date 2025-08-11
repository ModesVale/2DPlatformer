using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private float _groundCheckDistanse = 0.1f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpStrength;

    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _bodyCollider;
    private SpriteRenderer _sprite;
    private float _moveInput;
    private bool _isJumped;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _bodyCollider = GetComponent<CapsuleCollider2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        _isGrounded = IsGroundedRays();

        Vector2 currentVelocity = _rigidbody.velocity;
        currentVelocity.x = _moveInput * _moveSpeed;

        if (_isJumped && _isGrounded)
        {
            currentVelocity.y = _jumpStrength;
        }
        
        _rigidbody.velocity = currentVelocity;
        _isJumped = false;
    }

    private void HandleInput()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            _isJumped = true;
        }

        if (_sprite != null && _moveInput != 0)
        {
            _sprite.flipX = _moveInput < 0f;
        }
    }

    private bool IsGroundedRays()
    {
        Bounds bounds = _bodyCollider.bounds;

        Vector2 rayOrigin = new Vector2(bounds.center.x, bounds.min.y);

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, _groundCheckDistanse, _groundMask);

        Debug.DrawRay(rayOrigin, Vector2.down * _groundCheckDistanse, hit ? Color.green : Color.red);
        
        return hit.collider != null;
    }
}

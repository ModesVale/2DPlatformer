using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private Collider2D _groundSensor;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpStrength;
    [SerializeField] private Animator _animator;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private float _moveInput;
    private bool _isJumped;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        HandleInput();
        SetAnimation();
    }

    private void FixedUpdate()
    {
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

    private void SetGrounded(Collider2D collider, bool state)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _isGrounded = state;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) => SetGrounded(other, true);

    private void OnTriggerExit2D(Collider2D other) => SetGrounded(other, false);

    private void SetAnimation()
    {
        if (_animator != null)
        {
            _animator.SetFloat("Speed", Mathf.Abs(_moveInput));
            _animator.SetBool("IsGrounded", _isGrounded);
        }
    }
}

using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private Collider2D _groundSensor;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpStrength;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D _rigidbody;
    private CharacterInput _input;
    private float _moveInput;
    private bool _isJumped;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _input = GetComponent<CharacterInput>();
    }

    private void Update()
    {
        _moveInput = _input.Move;

        if (_input.Jump)
        {
            _isJumped = true;
        }

        SetAnimation();
        FlipDirection();
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

    private void SetGrounded(Collider2D collider, bool state)
    {
        if (((1 << collider.gameObject.layer) & _groundLayer) != 0)
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

    private void FlipDirection()
    {
        if (_moveInput == 0f) return;

        Vector2 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (_moveInput < 0 ? 1 : -1);
        transform.localScale = scale;
    }
}

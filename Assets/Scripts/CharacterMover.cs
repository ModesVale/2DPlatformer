using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _groundThreshold = 0.5f;

    private Rigidbody2D _rigidbody;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        _rigidbody.velocity = new Vector2(moveInput * _speed, _rigidbody.velocity.y);
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") &&  _isGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > _groundThreshold)
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isGrounded = false;
    }
}

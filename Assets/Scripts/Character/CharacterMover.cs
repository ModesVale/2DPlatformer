using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(CharacterInput))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpStrength;
    [SerializeField] private GroundSensor _groundSensor;
    [SerializeField] private Animator _animator;
    [SerializeField] private Flipper _flipper;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");

    private Rigidbody2D _rigidbody;
    private CharacterInput _input;
    private float _moveInput;
    private bool _jumpRequested;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _input = GetComponent<CharacterInput>();
    }

    private void Update()
    {
        HandleInput();
        UpdateAnimator();
        UpdateFacing();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void HandleInput()
    {
        _moveInput = _input.Move;

        if (_input.Jump)
        {
            _jumpRequested = true;
        }
    }

    private void ApplyMovement()
    {
        bool isGrounded = _groundSensor != null && _groundSensor.IsGrounded;

        Vector2 currentVelocity = _rigidbody.velocity;
        currentVelocity.x = _moveInput * _moveSpeed;

        if (_jumpRequested && isGrounded)
        {
            currentVelocity.y = _jumpStrength;
        }

        _rigidbody.velocity = currentVelocity;
        _jumpRequested = false;
    }

    private void UpdateAnimator()
    {
        if (_animator == null)
        {
            return;
        }

        _animator.SetFloat(SpeedHash, Mathf.Abs(_moveInput));
        _animator.SetBool(IsGroundedHash, _groundSensor != null && _groundSensor.IsGrounded);
    }

    private void UpdateFacing()
    {
        if (_flipper == null)
        {
            return;
        }

        if (_moveInput < 0)
        {
            _flipper.SetFacing(FacingDirection.Left);
        }
        else if (_moveInput > 0)
        {
            _flipper.SetFacing(FacingDirection.Right);
        }
    }
}

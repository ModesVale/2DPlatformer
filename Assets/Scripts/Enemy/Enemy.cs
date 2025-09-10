using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _patrolZone;
    [SerializeField] private float _speed = 2;
    [SerializeField] private int _startDirection = 1;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private int _direction;

    private void Awake()
    {
        _direction = Mathf.Sign(_startDirection) >= 0 ? 1 : -1;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        EnemyMove();
    }

    private void EnemyMove()
    {
        Bounds zone = _patrolZone.bounds;
        Bounds bodyCollider = _collider.bounds;
        float halfWidth = bodyCollider.extents.x;
        Vector2 position = _rigidbody.position;
        Vector2 velocity = _rigidbody.velocity;

        if (position.x - halfWidth <= zone.min.x && _direction < 0)
        {
            _direction = 1;
        }
        else if (position.x + halfWidth >= zone.max.x && _direction > 0)
        {
            _direction = -1;
        }

        velocity.x = _direction * _speed;
        _rigidbody.velocity = velocity;

        Vector2 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (_direction < 0 ? 1 : -1);
        transform.localScale = scale;
    }
}

using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _patrolZone;
    [SerializeField] private float _speed = 2;
    [SerializeField] private int _startDirection = 1;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private int _direction;

    private void Awake()
    {
        _direction = Mathf.Sign(_startDirection) >= 0 ? 1 : -1;
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        EnemyMove();
    }

    private void EnemyMove()
    {
        Bounds zone = _patrolZone.bounds;
        Bounds bodyCollider = _rigidbody.GetComponent<Collider2D>().bounds;
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

        if (_sprite != null)
        {
            _sprite.flipX = _direction > 0;
        }
    }
}

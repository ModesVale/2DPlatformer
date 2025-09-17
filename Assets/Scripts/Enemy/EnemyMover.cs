using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void MoveHorizontal(float speed)
    {
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = speed;
        _rigidbody.velocity = velocity;
    }

    public void Stop()
    {
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = 0f;
        _rigidbody.velocity = velocity;
    }
}

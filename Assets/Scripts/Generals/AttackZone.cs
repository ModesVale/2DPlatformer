using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class AttackZone : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private FactionType _targetFaction;
    [SerializeField] private float _attackCooldown = 1f;

    private float _lastAttackTime;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Time.time - _lastAttackTime < _attackCooldown)
            return;

        if (other.TryGetComponent(out Faction faction) && faction.Type == _targetFaction)
        {
            if (other.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
                _lastAttackTime = Time.time;
            }
        }
    }
}

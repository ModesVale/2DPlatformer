using UnityEngine;

[RequireComponent (typeof(Faction))]
public class SightSensor : MonoBehaviour
{
    [SerializeField] private float _viewRadius = 6f;
    [SerializeField] private int _maxTargetsCount = 4;
    [SerializeField] private LayerMask _obstacleLayers;
    [SerializeField] private FactionType _targetFaction;

    private Collider2D[] _detectedColliders;

    private void Awake()
    {
        _detectedColliders = new Collider2D[_maxTargetsCount];
    }

    public bool TryFindTarget(out Transform detectedTarget)
    {
        detectedTarget = null;
        
        int detectedCount = Physics2D.OverlapCircleNonAlloc(transform.position, _viewRadius, _detectedColliders);

        for (int i = 0;  i < detectedCount; i++)
        {
            Collider2D collider = _detectedColliders[i];

            if (collider == null)
            {
                continue;
            }
            
            if (collider.TryGetComponent(out Faction faction) && faction.Type == _targetFaction)
            {
                Vector2 directionToTarget = (Vector2)collider.transform.position - (Vector2)transform.position;
                float distanceToTarget = directionToTarget.magnitude;

                RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, directionToTarget.normalized, distanceToTarget, _obstacleLayers);

                if (hitInfo.collider == null)
                {
                    detectedTarget = collider.transform;
                    return true;
                }
            }
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
    }
}
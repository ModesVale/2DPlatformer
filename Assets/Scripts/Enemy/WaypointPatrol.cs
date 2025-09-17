using UnityEngine;

[RequireComponent (typeof(EnemyMover))]
[RequireComponent (typeof(Flipper))]
public class WaypointPatrol : MonoBehaviour
{
    [SerializeField] private Transform _waypointsRoot;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private FacingDirection _initialDirection;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _reachThreshold = 0.05f;

    private EnemyMover _mover;
    private Flipper _flipper;
    private int _currentIndex;
    private int _directionSign;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
        _flipper = GetComponent<Flipper>();

        InitializeWaypoints();
        InitializeStartPosition();
    }

    private void FixedUpdate()
    {
        Patrol();
    }

    private void InitializeWaypoints()
    {
        if (_waypointsRoot != null && (_waypoints == null || _waypoints.Length == 0))
        {
            int count = _waypointsRoot.childCount;
            _waypoints = new Transform[count];

            for (int i = 0; i < count; i++)
            {
                _waypoints[i] = _waypointsRoot.GetChild(i);
            }
        }
    }

    private void InitializeStartPosition()
    {
        _directionSign = _initialDirection == FacingDirection.Right ? DirectionConstants.Right : DirectionConstants.Left;

        _currentIndex = _directionSign == DirectionConstants.Right ? 0 : _waypoints.Length -1;
    }

    private void Patrol()
    {
        if (_waypoints == null || _waypoints.Length == 0)
        {
            return;
        }

        Transform target = _waypoints[_currentIndex];
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = target.position;

        float direction = Mathf.Sign(targetPosition.x - currentPosition.x);
        float horizontalSpeed = direction * _moveSpeed;

        if (Mathf.Abs(targetPosition.x - currentPosition.x) <= _reachThreshold)
        {
            SetNextTarget();
            _mover.Stop();
        }
        else
        {
            _mover.MoveHorizontal(horizontalSpeed);
            _flipper.SetFacing(direction >= 0 ? FacingDirection.Right : FacingDirection.Left);
        }
    }

    private void SetNextTarget()
    {
        if (_directionSign == DirectionConstants.Right)
        {
            _currentIndex = (_currentIndex + 1) % _waypoints.Length;
        }
        else
        {
            _currentIndex = (_currentIndex - 1 + _waypoints.Length) % _waypoints.Length;
        }
    }
}

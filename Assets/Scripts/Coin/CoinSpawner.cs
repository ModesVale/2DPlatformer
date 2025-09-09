using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _respawnDelay = 5f;
    [SerializeField] private int _poolCapacity = 10;

    private ObjectPool<Coin> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (coin) => coin.ResetState(),
            actionOnRelease: (coin) => coin.gameObject.SetActive(false),
            actionOnDestroy: (coin) => Destroy(coin.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity
        );
    }

    private void Start()
    {
        foreach (Transform point in _spawnPoints)
        {
            SpawnAtPoint(point);
        }
    }

    private void SpawnAtPoint(Transform point)
    {
        Coin coin = _pool.Get();
        coin.transform.position = point.position;
        coin.RegisterReturn(() => StartCoroutine(RespawnAtPoint(point)));
    }

    private IEnumerator RespawnAtPoint(Transform point)
    {
        yield return new WaitForSeconds( _respawnDelay );
        SpawnAtPoint(point);
    }
}

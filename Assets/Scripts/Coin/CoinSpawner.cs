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
            actionOnRelease: (coin) => coin.UnregisterReturn(),
            actionOnDestroy: (coin) => Destroy(coin.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity
        );
    }

    private void Start()
    {
        foreach (Transform spawnPoint in _spawnPoints)
        {
            SpawnAtPoint(spawnPoint);
        }
    }

    private void SpawnAtPoint(Transform spawnPoint)
    {
        Coin coin = _pool.Get();
        coin.transform.position = spawnPoint.position;
        coin.RegisterReturn(() => StartCoroutine(RespawnAtPoint(spawnPoint)));
    }

    private IEnumerator RespawnAtPoint(Transform spawnPoint)
    {
        yield return new WaitForSeconds( _respawnDelay );
        SpawnAtPoint(spawnPoint);
    }
}

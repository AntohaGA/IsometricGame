using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class PointSpawn : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private float _delay;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private LayerMask _targetLayerMask;

    private CapsuleCollider2D _capsuleCollider2D;
    private bool _isSpawned = false;
    private Coroutine _coroutine;
    private WaitForSeconds _spawnDelay;

    private void Awake()
    {
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _capsuleCollider2D.isTrigger = true;
        _spawnDelay = new WaitForSeconds(_delay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isSpawned && _coroutine == null && IsInTargetLayer(collision.gameObject))
        {
            _coroutine = StartCoroutine(SpawnEnemyCoroutine());
            _isSpawned = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsInTargetLayer(collision.gameObject) && _coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
            _isSpawned = false;
        }
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        int currentSpawn = _count;

        while (currentSpawn > 0)
        {
            if (_enemySpawner != null)
            {
                _enemySpawner.SpawnEnemy(_spawnTransform.position);
            }
            else
            {
                Debug.LogError("EnemySpawner не назначен в инспекторе!", this);
                yield break;
            }

            currentSpawn--;

            yield return _spawnDelay;
        }

        _coroutine = null;
    }

    private bool IsInTargetLayer(GameObject obj)
    {
        return (_targetLayerMask.value & (1 << obj.layer)) > 0;
    }
}
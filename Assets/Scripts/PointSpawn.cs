using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class PointSpawn : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private float _delay;
    [SerializeField] private EnemySpawner _enemySpawner;

    // 1. Добавляем переменную для маски слоя.
    // В инспекторе нужно будет выбрать слой "PlayerHitbox".
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
        // 3. Заменяем проверку тега на проверку слоя
        if (!_isSpawned && _coroutine == null && IsInTargetLayer(collision.gameObject))
        {
            _coroutine = StartCoroutine(SpawnEnemyCoroutine());
            _isSpawned = true;
        }
    }

    // 4. Останавливаем корутину, если объект нужного слоя вышел из триггера
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsInTargetLayer(collision.gameObject) && _coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
            _isSpawned = false; // Сбрасываем флаг для возможности повторного спавна
        }
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        int currentSpawn = _count;

        while (currentSpawn > 0)
        {
            if (_enemySpawner != null)
            {
                _enemySpawner.SpawnEnemy(transform.position);
            }
            else
            {
                Debug.LogError("EnemySpawner не назначен в инспекторе!", this);
                yield break; // Останавливаем корутину, если спавнер не назначен
            }

            currentSpawn--;

            // Используем оптимизированную задержку
            yield return _spawnDelay;
        }

        // Корутина завершена, сбрасываем ссылку
        _coroutine = null;
    }

    // Вспомогательный метод для проверки слоя
    private bool IsInTargetLayer(GameObject obj)
    {
        return (_targetLayerMask.value & (1 << obj.layer)) > 0;
    }
}
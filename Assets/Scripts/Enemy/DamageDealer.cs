using System.Collections;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [Header("Настройки урона")]
    [SerializeField] private float _damagePerHit = 10f;

    [Header("Настройки зоны")]
    [SerializeField] private float _damageRadius = 0.5f; // Радиус зоны урона
    [SerializeField] private LayerMask _targetLayerMask; // Маска слоя для поиска (например, PlayerHitbox)

    private readonly WaitForSeconds _damageDelay = new WaitForSeconds(0.5f);

    private void OnEnable()
    {
        StartCoroutine(CheckForTargets());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator CheckForTargets()
    {
        while (true)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _damageRadius, _targetLayerMask);

            foreach (var hitCollider in hitColliders)
            {
                // 2. Получаем Transform родителя найденного коллайдера
                // Проверяем, есть ли у коллайдера родитель, чтобы избежать ошибки NullReferenceException
                if (hitCollider.transform.parent == null)
                {
                    continue; // Пропускаем объект, если у него нет родителя
                }

                // 3. Ищем компонент Health на объекте родителя
                Health health = hitCollider.transform.parent.GetComponent<Health>();

                // 4. Если компонент найден, наносим урон
                if (health != null)
                {
                    health.TakeDamage((int)_damagePerHit);
                    Debug.Log($"Нанесено {_damagePerHit} урона!");
                    // Если Health найден на родителе, нет смысла искать дальше по иерархии вверх
                    continue;
                }
            }

            yield return _damageDelay;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _damageRadius);
    }
}
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float damagePerSecond = 100f; // Урон, наносимый каждую секунду контакта
    [SerializeField] private LayerMask playerLayer; // Маска слоя игрока
    [SerializeField] private Collider2D _collider2D;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем попадание коллайдера игрока
        if ((playerLayer.value & (1 << other.gameObject.layer)) != 0 && other.TryGetComponent<PlayerGirl>(out var player))
        {
            StartCoroutine(DamageOverTime(other));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        StopAllCoroutines();
    }

    private IEnumerator DamageOverTime(Collider2D target)
    {
        while (true)
        {
            // Наносим урон каждые полсекунды
            yield return new WaitForSeconds(0.5f); // задержка перед нанесением следующего удара

            // Проверяем доступность целевого объекта
            if (!target || !target.TryGetComponent<PlayerGirl>(out var player))
                break;

            // Получаем здоровье игрока
            if (player.GetComponent<Health>() is { } health)
            {
                health.TakeDamage((int)(damagePerSecond * Time.deltaTime)); // Уроном наносятся постепенно, пропорционально времени столкновения
            }
        }
    }
}
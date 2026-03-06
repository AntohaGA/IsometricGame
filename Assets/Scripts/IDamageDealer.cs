public interface  IDamageDealer
{
    void DealDamage(IDamagable target, int damageAmount)
    {
        if (target != null)
        {
            target.TakeDamage(damageAmount);
        }
    }
}
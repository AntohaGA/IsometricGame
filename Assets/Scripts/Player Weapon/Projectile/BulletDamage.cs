public class BulletDamage
{
    private readonly int _damage;
    private int _currentPenetrations = 0;
    private int _maxPenetrations;

    public BulletDamage(int damage)
    {
        _damage = damage;
    }

    public void Initialize(int maxPenetrations)
    {
        _maxPenetrations = maxPenetrations;
        _currentPenetrations = 0;
    }

    public bool GiveDamage(IDamagable damagable)
    {
        damagable.TakeDamage(_damage);
        _currentPenetrations++;

        return _currentPenetrations >= _maxPenetrations;
    }
}
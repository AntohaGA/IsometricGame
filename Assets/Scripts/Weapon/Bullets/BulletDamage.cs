public class BulletDamage
{
    private readonly Bullet _bullet;
    private int _currentPenetrations = 0;
    private int _maxPenetrations;

    public BulletDamage(Bullet bullet)
    {
        _bullet = bullet;
    }

    public void Initialize(int maxPenetrations)
    {
        _maxPenetrations = maxPenetrations;
        _currentPenetrations = 0;
    }

    public void GiveDamage(IDamagable damagable)
    {
        _currentPenetrations++;
        damagable.TakeDamage(_bullet.Damage);

        if (_currentPenetrations >= _maxPenetrations)
        {
            _bullet.DestroyBullet();
        }
    }
}
public class SniperRiffle : Weapon
{
    public override void Shoot()
    {
        Bullet bullet = Instantiate(_bulletPrefab, _bulletSpawner.position, _bulletSpawner.rotation);
        bullet.Fly(1, 20, 100);
    }
}
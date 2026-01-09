public class Granader : Weapon
{
    public override void Shoot()
    {
        Bullet bullet = Instantiate(_bulletPrefab, _bulletSpawner.position, _bulletSpawner.rotation);
        bullet.Fly(3, 4, 100);
    }
}
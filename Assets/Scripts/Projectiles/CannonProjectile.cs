public class CannonProjectile : Projectile
{
    private void Update()
    {
        var translation = transform.forward * MSpeed;
        transform.Translate(translation);
    }
}
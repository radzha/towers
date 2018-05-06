﻿namespace Progress
{
    public class CannonProjectile : Projectile
    {
        protected override Settings.Projectile.Type GetProjectileType()
        {
            return Settings.Projectile.Type.Ball;
        }
        
        private void Update()
        {
            var translation = transform.forward * Speed;
            transform.Translate(translation);
        }
    }
}
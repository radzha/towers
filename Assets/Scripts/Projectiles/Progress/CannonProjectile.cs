using UnityEngine;

namespace Progress
{
    public class CannonProjectile : Projectile
    {
        public override Settings.Projectile.Type GetProjectileType()
        {
            return Settings.Projectile.Type.Ball;
        }

        private void Update()
        {
            var translation = transform.forward * Speed * SpeedBoost * Time.deltaTime;
            transform.Translate(translation, Space.World);
        }

        public override void Reset(Vector3 position, Quaternion rotation, float speedBoost, GameObject target)
        {
            base.Reset(position, rotation, speedBoost, target);

            ResetVelocity();
        }

        private void ResetVelocity()
        {
            var body = GetComponent<Rigidbody>();
            if (body != null)
            {
                body.velocity = Vector3.zero;
                body.angularVelocity = Vector3.zero;
            }
        }
    }
}
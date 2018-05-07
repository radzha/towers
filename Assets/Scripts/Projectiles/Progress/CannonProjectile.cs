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

            var body = GetComponent<Rigidbody>();
            if (body != null)
            {
                body.velocity = new Vector3(0f, 0f, 0f);
                body.angularVelocity = new Vector3(0f, 0f, 0f);
            }
        }
    }
}
using UnityEngine;

namespace Progress
{
    public class GuidedProjectile : Projectile
    {
        private GameObject _target;

        public override Settings.Projectile.Type GetProjectileType()
        {
            return Settings.Projectile.Type.Crystal;
        }

        private void Update()
        {
            if (_target == null)
            {
                Explode();
                return;
            }

            var translation = _target.transform.position - transform.position;

            var currentSpeed = Speed * Time.deltaTime;

            if (translation.magnitude > currentSpeed)
            {
                translation = translation.normalized * currentSpeed;
            }

            transform.Translate(translation);
        }

        public override void Reset(Vector3 position, Quaternion rotation, float speedBoost, GameObject target)
        {
            base.Reset(position, rotation, speedBoost, target);
            _target = target;
        }
    }
}
using UnityEngine;

namespace Progress
{
    public class GuidedProjectile : Projectile
    {
        public GameObject MTarget;

        protected override Settings.Projectile.Type GetProjectileType()
        {
            return Settings.Projectile.Type.Crystal;
        }
        
        private void Update()
        {
            if (MTarget == null)
            {
                Destroy(gameObject);
                return;
            }

            var translation = MTarget.transform.position - transform.position;
            if (translation.magnitude > Speed)
            {
                translation = translation.normalized * Speed;
            }

            transform.Translate(translation);
        }
    }
}
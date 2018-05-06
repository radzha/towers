using UnityEngine;

namespace Progress
{
    public class GuidedProjectile : Projectile
    {
        public GameObject Target;

        protected override Settings.Projectile.Type GetProjectileType()
        {
            return Settings.Projectile.Type.Crystal;
        }
        
        private void Update()
        {
            if (Target == null)
            {
                Destroy(gameObject);
                return;
            }

            var translation = Target.transform.position - transform.position;
            if (translation.magnitude > Speed)
            {
                translation = translation.normalized * Speed;
            }

            transform.Translate(translation);
        }
    }
}
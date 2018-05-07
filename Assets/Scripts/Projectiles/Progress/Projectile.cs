using System.Linq;
using UnityEngine;

namespace Progress
{
    public abstract class Projectile : MonoBehaviour
    {
        protected float Speed;
        private float _damage;
        public float SpeedBoost = 1f;

        public abstract Settings.Projectile.Type GetProjectileType();

        private void Awake()
        {
            LevelEditor.Instance.OnSettingsUpdated -= SettingsRead;
            LevelEditor.Instance.OnSettingsUpdated += SettingsRead;

            SettingsRead();
        }

        /// <summary>
        /// Чтение настроек.
        /// </summary>
        private void SettingsRead()
        {
            var settings = new Settings.Projectile(GetProjectileType());
            Speed = settings.Speed;
            _damage = settings.Damage;
        }

        public static Projectile Create(Settings.Projectile.Type type)
        {
            var prefab = LevelEditor.Instance.Projectiles.FirstOrDefault(p => p.Type == type).Prefab;
            var o = Instantiate(prefab, LevelEditor.Instance.ProjectilesHolder);
            var projectile = o.GetComponent<Projectile>();
            return projectile;
        }

        private void OnTriggerEnter(Collider other)
        {
            var monster = other.gameObject.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeDamage(_damage);
            }

            if (other.gameObject.CompareTag(Constants.ExplodableTag))
            {
                Explode();
            }
        }

        protected void Explode()
        {
            ProjectileManager.Instance.Hide(this);
        }

        public virtual void Reset(Vector3 position, Quaternion rotation, float speedBoost, GameObject target)
        {
            transform.position = position;
            transform.rotation = rotation;
            SpeedBoost = speedBoost;
            SettingsRead();
        }
    }
}
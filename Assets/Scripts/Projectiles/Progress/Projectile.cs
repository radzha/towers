using System;
using System.Linq;
using Progress;
using UnityEngine;

namespace Progress
{
    public abstract class Projectile : MonoBehaviour
    {
        protected float Speed;
        private float _damage;
        public float SpeedBoost = 1f;

        protected abstract Settings.Projectile.Type GetProjectileType();

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

        public static GameObject Create(Settings.Projectile.Type type, Vector3 position, Quaternion rotation,
            float speedBoost)
        {
            var prefab = LevelEditor.Instance.Projectiles.FirstOrDefault(p => p.Type == type).Prefab;
            var o = Instantiate(prefab, position, rotation, LevelEditor.Instance.ProjectilesHolder);
            var projectile = o.GetComponent<Projectile>();
            projectile.SpeedBoost = speedBoost;
            return o;
        }

        private void OnTriggerEnter(Collider other)
        {
            var monster = other.gameObject.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeDamage(_damage);
            }

            if (other.gameObject.CompareTag("Explodable"))
            {
                Destroy(gameObject);
            }
        }
    }
}
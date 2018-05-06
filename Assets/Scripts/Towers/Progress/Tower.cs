using System;
using UnityEngine;

namespace Progress
{
    public abstract class Tower : MonoBehaviour
    {
        [SerializeField] private float _shootInterval = 0.5f;
        [SerializeField] private float _range = 4f;

        private float _lastShotTime = -0.5f;

        protected abstract Settings.Tower.TowerType GetTowerType();
        protected abstract Vector3 GetShootPosition();
        protected abstract Quaternion GetShootRotation();

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
            var settings = new Settings.Tower(GetTowerType());

            _shootInterval = settings.ShootInterval;
            _range = settings.AttackRange;
        }

        private void Update()
        {
            if (_lastShotTime + _shootInterval <= Time.time)
            {
                foreach (var monster in MonsterManager.Instance.GetActiveMonsters())
                {
                    if (Vector3.Distance(transform.position, monster.transform.position) <= _range)
                    {
                        MakeShot(monster);
                        break;
                    }
                }
            }
        }

        private void MakeShot(Monster monster)
        {
            var projectile = Projectile.Create(GetProjectileType(), GetShootPosition(), GetShootRotation());

            HandleProjectile(projectile, monster);

            _lastShotTime = Time.time;
        }

        protected virtual void HandleProjectile(GameObject projectile, Monster monster)
        {
            //do nothing
        }

        private Settings.Projectile.Type GetProjectileType()
        {
            switch (GetTowerType())
            {
                case Settings.Tower.TowerType.Simple:
                    return Settings.Projectile.Type.Crystal;
                case Settings.Tower.TowerType.Cannon:
                default:
                    return Settings.Projectile.Type.Ball;
            }
        }
    }
}
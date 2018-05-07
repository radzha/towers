using System;
using UnityEngine;

namespace Progress
{
    public abstract class Tower : MonoBehaviour
    {
        private float _shootInterval;
        protected float _range;
        protected float _turningSpeed;

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
            _turningSpeed = settings.TurningSpeed;
        }

        protected void MakeShot(Monster monster, float speedBoost = 1f)
        {
            var projectile = ProjectileManager.Instance.GetNext(GetProjectileType(), GetShootPosition(),
                GetShootRotation(), speedBoost, monster);

//            HandleProjectile(projectile, monster);

            _lastShotTime = Time.time;
        }

        protected virtual void HandleProjectile(GameObject projectile, Monster monster)
        {
            //do nothing
        }

        protected Settings.Projectile.Type GetProjectileType()
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

        protected bool CanShoot()
        {
            return _lastShotTime + _shootInterval <= Time.time;
        }

        protected float DistanceWith(Monster monster)
        {
            return Vector3.Distance(transform.position, monster.transform.position);
        }
    }
}
using UnityEngine;

namespace Progress
{
    public abstract class Tower : MonoBehaviour
    {
        private float _shootInterval;
        private float _range;
        protected float _turningSpeed;
        private Color _targetColor;

        private float _lastShotTime = float.MinValue;

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
            _targetColor = settings.TargetColor;
        }

        protected void MakeShot(Monster monster, float speedBoost = 1f)
        {
            ProjectileManager.Instance.GetNext(GetProjectileType(), GetShootPosition(), GetShootRotation(), speedBoost,
                monster);

            _lastShotTime = Time.time;
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

        private float DistanceWith(Component monster)
        {
            return Vector3.Distance(transform.position, monster.transform.position);
        }

        /// <summary>
        /// Находит ближайшего монстра.
        /// Можно реализовать и что-то более продвинутое,
        /// например не выбирать монстра, который спасется раньше, чем долетит снаряд.
        /// </summary>
        /// <returns></returns>
        protected Monster GetNearestMonster()
        {
            Monster nearest = null;
            var minDist = float.MaxValue;
            foreach (var monster in MonsterManager.Instance.GetActiveMonsters())
            {
                var distance = DistanceWith(monster);
                if (distance <= _range && distance <= minDist)
                {
                    nearest = monster;
                    minDist = distance;
                }
            }

            return nearest;
        }

        protected void MarkAsTarget(Monster target)
        {
            target.MarkAsTarget(_targetColor);
        }
    }
}
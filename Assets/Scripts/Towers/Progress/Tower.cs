using UnityEngine;

namespace Progress
{
    public abstract class Tower : MonoBehaviour
    {
        [SerializeField] private float _shootInterval = 0.5f;
        [SerializeField] private float _range = 4f;
        [SerializeField] private GameObject _projectilePrefab;

        private float _mLastShotTime = -0.5f;

        protected abstract Settings.Tower.TowerType GetType();
        protected abstract Vector3 GetShootPosition();
        protected abstract Quaternion GetShootRotation();

        protected virtual bool MissingPrefabs()
        {
            return _projectilePrefab == null;
        }

        private void Awake()
        {
            SettingsRead(GetType());
        }
        
        /// <summary>
        /// Чтение настроек.
        /// </summary>
        protected void SettingsRead(Settings.Tower.TowerType type)
        {
            var settings = new Settings.Tower(type);
            _shootInterval = settings.ShootInterval;
            _range = settings.AttackRange;
            _projectilePrefab = settings.ProjectilePrefab;
        }

        private void Update()
        {
            if (MissingPrefabs()) return;

            foreach (var monster in FindObjectsOfType<Monster>())
            {
                if (Vector3.Distance(transform.position, monster.transform.position) > _range) continue;

                if (_mLastShotTime + _shootInterval > Time.time) continue;

                MakeShot(monster);
            }
        }

        private void MakeShot(Monster monster)
        {
            var projectile = Instantiate(_projectilePrefab, GetShootPosition(), GetShootRotation());

            HandleProjectile(projectile, monster);

            _mLastShotTime = Time.time;
        }

        protected virtual void HandleProjectile(GameObject projectile, Monster monster)
        {
            //do nothing
        }
    }
}
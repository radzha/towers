using UnityEngine;

namespace Progress
{
    public abstract class Tower : MonoBehaviour
    {
        [SerializeField] private float _mShootInterval = 0.5f;
        [SerializeField] private float _mRange = 4f;
        [SerializeField] private GameObject _mProjectilePrefab;

        private float _mLastShotTime = -0.5f;

        protected virtual bool MissingPrefabs()
        {
            return _mProjectilePrefab == null;
        }

        protected abstract Vector3 GetShootPosition();
        protected abstract Quaternion GetShootRotation();

        private void Update()
        {
            if (MissingPrefabs()) return;

            foreach (var monster in FindObjectsOfType<Monster>())
            {
                if (Vector3.Distance(transform.position, monster.transform.position) > _mRange) continue;

                if (_mLastShotTime + _mShootInterval > Time.time) continue;

                MakeShot(monster);
            }
        }

        private void MakeShot(Monster monster)
        {
            var projectile = Instantiate(_mProjectilePrefab, GetShootPosition(), GetShootRotation());

            HandleProjectile(projectile, monster);

            _mLastShotTime = Time.time;
        }

        protected virtual void HandleProjectile(GameObject projectile, Monster monster)
        {
            //do nothing
        }
    }
}
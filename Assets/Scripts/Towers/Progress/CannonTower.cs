using UnityEngine;

namespace Progress
{
    public class CannonTower : Tower
    {
        [SerializeField] private Transform _mShootPoint;

        protected override bool MissingPrefabs()
        {
            return base.MissingPrefabs() || _mShootPoint == null;
        }

        protected override Settings.Tower.TowerType GetType()
        {
            return Settings.Tower.TowerType.Cannon;
        }

        protected override Vector3 GetShootPosition()
        {
            return _mShootPoint.position;
        }

        protected override Quaternion GetShootRotation()
        {
            return _mShootPoint.rotation;
        }
    }
}
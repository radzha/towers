using UnityEngine;

namespace Progress
{
    public class CannonTower : Tower
    {
        [SerializeField] private Transform _shootPoint;

        protected override bool MissingPrefabs()
        {
            return base.MissingPrefabs() || _shootPoint == null;
        }

        protected override Settings.Tower.TowerType GetTowerType()
        {
            return Settings.Tower.TowerType.Cannon;
        }

        protected override Vector3 GetShootPosition()
        {
            return _shootPoint.position;
        }

        protected override Quaternion GetShootRotation()
        {
            return _shootPoint.rotation;
        }
    }
}
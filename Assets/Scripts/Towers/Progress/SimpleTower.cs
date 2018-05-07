using Progress;
using UnityEngine;

namespace Progress
{
    public class SimpleTower : Tower
    {
        protected override Settings.Tower.TowerType GetTowerType()
        {
            return Settings.Tower.TowerType.Simple;
        }

        protected override Vector3 GetShootPosition()
        {
            return transform.position + Vector3.up * 1.5f;
        }

        protected override Quaternion GetShootRotation()
        {
            return Quaternion.identity;
        }

        private void Update()
        {
            if (CanShoot())
            {
                var target = GetNearestMonster(_range);

                if (target != null) MakeShot(target);
            }
        }
    }
}
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

        protected override void HandleProjectile(GameObject projectile, Monster monster)
        {
            var projectileBeh = projectile.GetComponent<GuidedProjectile>();

            projectileBeh.Target = monster.gameObject;
        }

        private void Update()
        {
            if (CanShoot())
            {
                foreach (var monster in MonsterManager.Instance.GetActiveMonsters())
                {
                    if (DistanceWith(monster) <= _range)
                    {
                        MakeShot(monster);
                        break;
                    }
                }
            }
        }
    }
}
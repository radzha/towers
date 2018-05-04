﻿using Progress;
using UnityEngine;

namespace Progress
{
    public class SimpleTower : Tower
    {
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

            projectileBeh.MTarget = monster.gameObject;
        }
    }
}
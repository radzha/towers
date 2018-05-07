using System;
using System.Linq;
using UnityEngine;

namespace Progress
{
    public class CannonTower : Tower
    {
        [SerializeField] private Transform _shootPoint;

        private Monster _target;

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

        private void Update()
        {
            if (_target != null && !_target.IsAlive()) _target = null;

            if (_target == null) _target = GetNearestMonster(_range);

            if (_target != null)
            {
                _target.SetColor(Color.cyan);
                var turningSpeed = GetNeededTurningSpeed();
                if (turningSpeed != null)
                {
                    Turn((float) turningSpeed);
                }
                else
                {
                    if (CanShoot())
                    {
                        MakeShot(_target, GetSpeedBoost());
                    }
                }
            }
        }

        private void Turn(float turningSpeed)
        {
            transform.Rotate(Vector3.up, turningSpeed);
        }

        private float? GetNeededTurningSpeed()
        {
            float timeBeforeExplode = GetTimeBeforeExplode();
            Vector3 targetPredictedPosition = TargetPredictedPosition(timeBeforeExplode);

            var target = targetPredictedPosition - transform.position;
            var cannon = transform.rotation * Vector3.forward;
            target.y = cannon.y = 0f;

            var angle = SignedAngle(cannon, target, Vector3.up);

            var maxAngle = _turningSpeed * Time.deltaTime;
            if (Mathf.Abs(angle) >= maxAngle)
            {
                angle = maxAngle * Mathf.Sign(angle);
            }
            else if (Mathf.Abs(angle) < 0.1f)
            {
                return null;
            }

            return angle;
        }

        private Vector3 TargetPredictedPosition(float timeBeforeExplode)
        {
            var current = _target.gameObject.transform.position;
            var direction = _target.TargetPosition - current;
            var shift = direction.normalized * timeBeforeExplode;
            return current + shift;
        }

        private float GetTimeBeforeExplode()
        {
            var shotHeight = _shootPoint.position.y;
            var gravity = 9.81f;
            var time = Mathf.Sqrt(shotHeight * 2f / gravity);
            return time;
        }

        private float GetSpeedBoost()
        {
            var time = GetTimeBeforeExplode();
            var targetPosition = TargetPredictedPosition(time);
            Debug.DrawLine(_shootPoint.position, targetPosition);
            var speed = Vector3.Distance(targetPosition, _shootPoint.position) / time;
            var projectileSpeed = LevelEditor.Instance.Projectiles.FirstOrDefault(p => p.Type == GetProjectileType())
                .Speed;
            var boost = speed / projectileSpeed;
            return boost;
        }

        // Вспомогательные функции из Unity 2017
        private float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)
        {
            var unsignedAngle = Vector3.Angle(from, to);
            var sign = Mathf.Sign(Dot(axis, Cross(from, to)));
            return unsignedAngle * sign;
        }

        private static float Dot(Vector3 lhs, Vector3 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
        }

        private static Vector3 Cross(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(
                lhs.y * rhs.z - lhs.z * rhs.y,
                lhs.z * rhs.x - lhs.x * rhs.z,
                lhs.x * rhs.y - lhs.y * rhs.x);
        }
    }
}
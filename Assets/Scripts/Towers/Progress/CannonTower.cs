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

            if (_target == null)
            {
                _target = GetNearestMonster();

                if (_target != null) MarkAsTarget(_target);
            }

            if (_target != null)
            {
                bool isFinalTurn;

                var turningAngle = GetNeededTurningAngle(out isFinalTurn);

                Turn(turningAngle);

                if (isFinalTurn && CanShoot())
                {
                    MakeShot(_target, GetSpeedBoost());
                }
            }
        }

        private void Turn(float turningAngle)
        {
            transform.Rotate(Vector3.up, turningAngle);
        }

        /// <summary>
        /// Вычисляет поворот на данном кадре.
        /// </summary>
        /// <param name="isFinalTurn"> Если поворот последний, то true </param>
        /// <returns>Угол в градусах</returns>
        private float GetNeededTurningAngle(out bool isFinalTurn)
        {
            var timeBeforeExplode = GetTimeBeforeExplode;
            var targetPredictedPosition = TargetPredictedPosition(timeBeforeExplode);

            var target = targetPredictedPosition - transform.position;
            var cannon = transform.rotation * Vector3.forward;
            target.y = cannon.y = 0f;

            var angle = SignedAngle(cannon, target, Vector3.up);

            var maxAngle = _turningSpeed * Time.deltaTime;

            if (Mathf.Abs(angle) > maxAngle)
            {
                angle = maxAngle * Mathf.Sign(angle);
                isFinalTurn = false;
            }
            else
            {
                isFinalTurn = true;
            }

            return angle;
        }

        private Vector3 TargetPredictedPosition(float timeBeforeExplode)
        {
            var current = _target.gameObject.transform.position;
            var direction = _target.TargetPosition - current;
            var shift = direction.normalized * _target.Speed * timeBeforeExplode;

            Debug.DrawLine(current, current + shift, Color.green);

            return current + shift;
        }

        private float _getTimeBeforeExplode = -1f;

        /// <summary>
        /// Время до взрыва ядра от свободного падения на пол.
        /// </summary>
        private float GetTimeBeforeExplode
        {
            get
            {
                if (_getTimeBeforeExplode < 0f)
                {
                    var ballSize = GetProjectileSettings().Prefab.transform.localScale.y;
                    var shotHeight = _shootPoint.position.y - ballSize;
                    var gravity = Mathf.Abs(Physics.gravity.y);
                    _getTimeBeforeExplode = Mathf.Sqrt(shotHeight * 2f / gravity);
                }

                return _getTimeBeforeExplode;
            }
        }

        private float GetSpeedBoost()
        {
            var time = GetTimeBeforeExplode;
            var targetPosition = TargetPredictedPosition(time);
            var speed = Vector3.Distance(targetPosition, _shootPoint.position) / time;
            var projectileSpeed = GetProjectileSettings()
                .Speed;
            return speed / projectileSpeed;
        }

        private LevelEditor.Projectile GetProjectileSettings()
        {
            return LevelEditor.Instance.Projectiles.FirstOrDefault(p => p.Type == GetProjectileType());
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
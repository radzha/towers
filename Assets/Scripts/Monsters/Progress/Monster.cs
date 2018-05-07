using System.Collections;
using UnityEngine;

namespace Progress
{
    public class Monster : MonoBehaviour, IDamagable
    {
        // Текущий показатель жизни.
        private float _health;

        // Скорость.
        public float Speed { get; private set; }

        // Дистанция характеризующая приближение к цели монстра.
        private float _reachDistance;

        // Основной цвет неатакованного монстра.
        private Color _color;

        // Цель монстра.
        public Vector3 TargetPosition { get; set; }

        private const float DamagingShowPeriod = 0.2f;

        private void Awake()
        {
            LevelEditor.Instance.OnSettingsUpdated -= SettingsRead;
            LevelEditor.Instance.OnSettingsUpdated += SettingsRead;

            Reset();
        }

        /// <summary>
        /// Чтение настроек.
        /// </summary>
        private void SettingsRead()
        {
            var settings = new Settings.Monster();
            _health = settings.Health;
            Speed = settings.Speed;
            _reachDistance = settings.ReachDistance;
            _color = settings.MonsterColor;
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, TargetPosition) <= _reachDistance)
            {
                Die(null);
                return;
            }

            var translation = TargetPosition - transform.position;


            var currentSpeed = Speed * Time.deltaTime;
            if (translation.magnitude > currentSpeed)
            {
                translation = translation.normalized * currentSpeed;
            }

            transform.Translate(translation);
        }

        public void Reset()
        {
            SettingsRead();
        }

        public float Health()
        {
            return _health;
        }

        public Color GetColor()
        {
            return _color;
        }

        public void TakeDamage(float damage, Settings.Projectile.Type type)
        {
            _health -= damage;

            if (_health <= 0)
            {
                Die(type);
            }
            else
            {
                "Monster damaged".Log(type + " / " + damage);
                StartCoroutine(ChangeColor());
            }
        }

        private IEnumerator ChangeColor()
        {
            SetColor(Color.white);

            yield return new WaitForSeconds(DamagingShowPeriod);

            if (gameObject.activeSelf) SetColor(_color);
        }

        public void Die(Settings.Projectile.Type? type)
        {
            MonsterManager.Instance.HideMonster(this);
            if (type != null)
            {
                "Monster killed".CLog(type);
            }
            else
            {
                "Monster escaped".CLogRed(type);
            }
        }

        public bool IsAlive()
        {
            return MonsterManager.Instance.GetActiveMonsters().Contains(this);
        }

        public void MarkAsTarget(Color color)
        {
            _color = color;
            SetColor(color);
        }

        private void SetColor(Color color)
        {
            var meshRenderer = gameObject.GetComponent<MeshRenderer>();
            meshRenderer.material.color = color;
        }
    }
}
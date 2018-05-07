using UnityEngine;

namespace Progress
{
    public class Monster : MonoBehaviour, IDamagable
    {
        // Текущий показатель жизни.
        private float _health;

        // Скорость.
        private float _speed;

        // Дистанция характеризующая приближение к цели монстра.
        private float _reachDistance;

        // Основной цвет неатакованного монстра.
        private Color _color;

        // Цель монстра.
        public Vector3 TargetPosition { get; set; }

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
            _speed = settings.Speed;
            _reachDistance = settings.ReachDistance;
            _color = settings.MonsterColor;
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, TargetPosition) <= _reachDistance)
            {
                Die();
                return;
            }

            var translation = TargetPosition - transform.position;
            
            
            var currentSpeed = _speed * Time.deltaTime;
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

        public void TakeDamage(float damage)
        {
            _health -= damage;
            
            if (_health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            MonsterManager.Instance.HideMonster(this);
        }

        public bool IsAlive()
        {
            return MonsterManager.Instance.GetActiveMonsters().Contains(this);
        }

        public void MarkAsTarget(Color color)
        {
            _color = color;
            
            var monsterObject = gameObject;
            var meshRenderer = monsterObject.GetComponent<MeshRenderer>();
            meshRenderer.material.color = _color;
        }
    }
}
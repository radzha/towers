using UnityEngine;

namespace Progress
{
    public class Monster : MonoBehaviour, Damagable
    {
        // Набор настроек монстра.
        public Settings.Monster Settings { get; set; }

        // Текущий показатель жизни.
        private float _health;

        // Скорость.
        private float _speed;

        // Дистанция достажения цели.
        private float _reachDistance = 0.3f;

        // Основной цвет.
        private Color _color;

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
            Settings = new Settings.Monster();
            _health = Settings.Health;
            _speed = Settings.Speed;
            _reachDistance = Settings.ReachDistance;
            _color = Settings.MonsterColor;
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, TargetPosition) <= _reachDistance)
            {
                Die();
                return;
            }

            var translation = TargetPosition - transform.position;
            if (translation.magnitude > _speed)
            {
                translation = translation.normalized * _speed;
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

        public float MaxHealth()
        {
            return Settings.Health;
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
    }
}
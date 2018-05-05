using UnityEngine;

namespace Progress
{
    public class Monster : MonoBehaviour, Damagable
    {
        // Набор настроек монстра.
        public Settings.Monster Settings { get; set; }

        // Текущий показатель жизни.
        private int _health;

        // Скорость.
        private float _speed;

        // Дистанция достажения цели.
        private float _reachDistance = 0.3f;

        // Основной цвет.
        private Color _color;

        public Vector3 TargetPosition { private get; set; }

        private void Awake()
        {
            SettingsRead();
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
                Destroy(gameObject);
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
//            Health = MaxHealth();
        }

        public int Health()
        {
            return _health;
        }

        public int MaxHealth()
        {
            return Settings.Health;
        }

        public Color GetColor()
        {
            return _color;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
        }

        public void OnDie()
        {
            throw new System.NotImplementedException();
        }

        public bool IsDead()
        {
            return _health > 0;
        }
    }
}
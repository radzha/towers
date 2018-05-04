using UnityEngine;

namespace Progress
{
    public class Monster : MonoBehaviour, Damagable
    {
        // Текущий показатель жизни.
        protected int health;

        /// <summary>
        /// Скорость.
        /// </summary>
        public float Speed { get; set; }

        // Основной цвет.
        private Color unitColor;

        // Набор настроек юнита.
        public Settings.Monster Settings { get; set; }

        public Vector3 TargetPosition { get; set; }

        private float ReachDistance = 0.3f;

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
            health = Settings.Health;
            Speed = Settings.Speed;
            ReachDistance = Settings.ReachDistance;
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, TargetPosition) <= ReachDistance)
            {
                Destroy(gameObject);
                return;
            }

            var translation = TargetPosition - transform.position;
            if (translation.magnitude > Speed)
            {
                translation = translation.normalized * Speed;
            }

            transform.Translate(translation);
        }

        public void Reset()
        {
//            Health = MaxHealth();
        }

        public int Health()
        {
            return health;
        }

        public int MaxHealth()
        {
            return Settings.Health;
        }

        public void OnDie()
        {
            throw new System.NotImplementedException();
        }

        public bool IsDead()
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
        }
    }
}
using UnityEngine;

namespace Progress
{
    public class Spawner : MonoBehaviour
    {
        public Settings.Spawner Settings { get; set; }

        private float _interval;

        [SerializeField] private Transform _moveTarget;

        private float _lastSpawnTime = -1;

        private void Awake()
        {
            LevelEditor.Instance.OnSettingsUpdated -= SettingsRead;
            LevelEditor.Instance.OnSettingsUpdated += SettingsRead;
            
            SettingsRead();
        }

        /// <summary>
        /// Чтение настроек.
        /// </summary>
        private void SettingsRead()
        {
            Settings = new Settings.Spawner();
            _interval = Settings.Interval;
            _moveTarget = Settings.MoveTarget;
        }

        private void Update()
        {
            if (Time.time > _lastSpawnTime + _interval)
            {
                MonsterManager.Instance.CreateMonster(transform.position, _moveTarget.position);
                _lastSpawnTime = Time.time;
            }
        }
    }
}
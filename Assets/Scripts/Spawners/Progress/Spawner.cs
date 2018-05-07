using UnityEngine;

namespace Progress
{
    /// <summary>
    /// Фабрика монстров.
    /// </summary>
    public class Spawner : MonoBehaviour
    {
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
            var settings = new Settings.Spawner();
            _interval = settings.Interval;
            _moveTarget = settings.MoveTarget;
        }

        private void Update()
        {
            if (Time.time > _lastSpawnTime + _interval || _lastSpawnTime < 0)
            {
                MonsterManager.Instance.CreateMonster(transform.position, _moveTarget.position);
                _lastSpawnTime = Time.time;
            }
        }
    }
}
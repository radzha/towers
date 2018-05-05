using UnityEngine;

namespace Settings
{
    /// <summary>
    /// Базовые настройки фабрики монстров.
    /// </summary>
    public class Spawner
    {
        /// <summary>
        /// Интервал споуна.
        /// </summary>
        public float Interval { get; set; }

        /// <summary>
        /// Точка цели движения.
        /// </summary>
        public Transform MoveTarget { get; set; }

        /// <summary>
        /// Прочитать настройки из редактора уровней.
        /// </summary>
        private void ReadSettings(LevelEditor.Spawner[] spawnerSettings, int level)
        {
            var spawner = spawnerSettings[level];

            Interval = spawner.Interval;
            MoveTarget = spawner.MoveTarget;
        }

        /// <summary>
        /// Первичное заполнение настроек.
        /// </summary>
        public Spawner()
        {
            // Если будут разные виды/уровни споунеров, нужно будет передавать не 0.
            ReadSettings(LevelEditor.Instance.Spawners, 0);
        }
    }
}
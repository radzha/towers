using UnityEngine;

namespace Settings
{
    /// <summary>
    /// Базовые настройки монстра.
    /// </summary>
    public class Monster
    {
        /// <summary>
        /// Количество жизней
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Скорость передвижения.
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// Дистанция поражения монстра.
        /// </summary>
        public float ReachDistance { get; set; }

        /// <summary>
        /// Цвет монстра.
        /// </summary>
        public Color MonsterColor { get; set; }

        /// <summary>
        /// Прочитать настройки из редактора уровней.
        /// </summary>
        /// <param name="monsterSettings">Набор настроек.</param>
        /// <param name="level">Уровень.</param>
        private void ReadSettings(LevelEditor.MonsterSettings[] monsterSettings, int level)
        {
            var monster = monsterSettings[level];
            Health = monster.Health;
            Speed = monster.Speed;
            ReachDistance = monster.ReachDistance;
            MonsterColor = monster.MonsterColor;
        }

        /// <summary>
        /// Первичное заполнение настроек.
        /// </summary>
        public Monster()
        {
            var monsters = LevelEditor.Instance.Monsters;
            // Выбираем случайного монстра из доступных. Можно ввести веса, если потребуется.
            ReadSettings(monsters, Random.Range(0, monsters.Length));
        }
    }
}
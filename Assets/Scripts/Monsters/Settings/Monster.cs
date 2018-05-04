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
        }

        /// <summary>
        /// Первичное заполнение настроек.
        /// </summary>
        public Monster ()
        {
            // Если будут уровни монстров, нужно будет передавать не 0.
            ReadSettings(LevelEditor.Instance.Monsters, 0);
        }
    }
}
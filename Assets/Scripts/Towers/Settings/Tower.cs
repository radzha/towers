﻿using System.Linq;
using UnityEngine;

namespace Settings
{
    public class Tower
    {
        public enum TowerType
        {
            Simple,
            Cannon
        }

        private TowerType Type { get; set; }
        public float ShootInterval { get; private set; }
        public float AttackRange { get; private set; }
        public float TurningSpeed { get; private set; }
        public Color TargetColor { get; private set; }

        /// <summary>
        /// Прочитать настройки из редактора уровней.
        /// </summary>
        private void ReadSettings(LevelEditor.Tower tower)
        {
            Type = tower.TowerType;
            ShootInterval = tower.ShootInterval;
            AttackRange = tower.AttackRange;
            TurningSpeed = tower.TurningSpeed;
            TargetColor = tower.TargetColor;
        }

        /// <summary>
        /// Первичное заполнение настроек.
        /// </summary>
        public Tower(TowerType type)
        {
            var towers = LevelEditor.Instance.Towers;

            // Выбираем первую подходящую башню из доступных
            ReadSettings(towers.FirstOrDefault(t => t.TowerType == type));
        }
    }
}
using System.Linq;
using UnityEngine;

namespace Settings
{
    public class Projectile
    {
        public enum Type
        {
            Ball,
            Crystal
        }

        /// <summary>
        /// Скорость снаряда.
        /// </summary>
        public float Speed { get; private set; }

        /// <summary>
        /// Урон.
        /// </summary>
        public float Damage { get; private set; }

        /// <summary>
        /// Префаб снаряда.
        /// </summary>
        public GameObject Prefab { get; private set; }

        /// <summary>
        /// Прочитать настройки из редактора уровней.
        /// </summary>
        /// <param name="monsterSettings">Набор настроек.</param>
        /// <param name="level">Уровень.</param>
        private void ReadSettings(LevelEditor.Projectile projectile)
        {
            Speed = projectile.Speed;
            Damage = projectile.Damage;
            Prefab = projectile.Prefab;
        }

        /// <summary>
        /// Первичное заполнение настроек.
        /// </summary>
        public Projectile(Type type)
        {
            var projectiles = LevelEditor.Instance.Projectiles;
            ReadSettings(projectiles.FirstOrDefault(p => p.Type == type));
        }
    }
}
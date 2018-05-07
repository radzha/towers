using System;
using UnityEngine;

/// <summary>
/// Редактор уровней. Заполняется значениями в инспекторе.
/// </summary>
public class LevelEditor : SingletonSimple<LevelEditor>
{
    /// <summary>
    /// Ивент, сообщающий подписчикам, что настройки изменились и нужно их перегрузить.
    /// </summary>
    public event Action OnSettingsUpdated;

    // Настройки монстров.
    [Serializable]
    public struct MonsterSettings
    {
        public int Health;
        public float Speed;
        public float ReachDistance;
        public Color MonsterColor;
    }

    // Настройки фабрики монстров.
    [Serializable]
    public struct Spawner
    {
        public float Interval;
        public Transform MoveTarget;
    }

    // Настройки башень.
    [Serializable]
    public struct Tower
    {
        public Settings.Tower.TowerType TowerType;
        public float ShootInterval;
        public float AttackRange;
        public float TurningSpeed;
        public Color TargetColor;
    }
    
    // Настройки снарядов.
    [Serializable]
    public struct Projectile
    {
        public Settings.Projectile.Type Type;
        public float Damage;
        public float Speed;
        public GameObject Prefab;
    }
    
    // Массив настроек монстров. Их может быть много, если есть потребность делить их по уровням, например.
    [Header("==== Monsters ====")] public MonsterSettings[] Monsters;

    // Массив настроек фабрик монстров. Их тоже может быть много, если хотим, чтобы было несколько точек с разными характеристиками.
    [Header("==== Spawners ====")] public Spawner[] Spawners;

    // Массив настроек башень.
    [Header("==== Towers ====")] public Tower[] Towers;
    
    // Массив настроек снарядов.
    [Header("==== Projectiles ====")] public Projectile[] Projectiles;

    [Header("==== Other settings ====")] 
    public Transform MonstersHolder;
    public Transform ProjectilesHolder;

    private void OnValidate()
    {
        if (OnSettingsUpdated != null)
        {
            OnSettingsUpdated.Invoke();
        }
    }
}
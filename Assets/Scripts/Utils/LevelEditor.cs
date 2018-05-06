using System;
using UnityEngine;

/// <summary>
/// Редактор уровней. Заполняется значениями в инспекторе.
/// </summary>
public class LevelEditor : SingletonSimple<LevelEditor>
{
    public event Action OnSettingsUpdated;

    // Настройки монстров.
    [System.Serializable]
    public struct MonsterSettings
    {
        public int Health;
        public float Speed;
        public float ReachDistance;
        public Color MonsterColor;
    }

    // Настройки фабрики монстров.
    [System.Serializable]
    public struct Spawner
    {
        public float Interval;
        public Transform MoveTarget;
    }

    // Настройки башень.
    [System.Serializable]
    public struct Tower
    {
        public Settings.Tower.TowerType TowerType;
        public float ShootInterval;
        public float AttackRange;
        public GameObject ProjectilePrefab;
    }

    // Массив настроек монстров. Их может быть много, если есть потребность делить их по уровням, например.
    [Header("==== Monsters ====")] public MonsterSettings[] Monsters;

    // Массив настроек фабрик монстров. Их тоже может быть много, если хотим, чтобы было несколько точек с разными характеристиками.
    [Header("==== Spawners ====")] public Spawner[] Spawners;

    // Массив настроек башень.
    [Header("==== Towers ====")] public Tower[] Towers;

    private void OnValidate()
    {
        if (OnSettingsUpdated != null)
        {
            OnSettingsUpdated.Invoke();
        }
    }
}
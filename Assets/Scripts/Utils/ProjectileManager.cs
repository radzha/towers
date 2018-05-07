using System.Collections.Generic;
using Progress;
using UnityEngine;

public class ProjectileManager : SingletonAuto<ProjectileManager>
{
    private readonly Dictionary<Settings.Projectile.Type, Stack<Projectile>> _poolDict =
        new Dictionary<Settings.Projectile.Type, Stack<Projectile>>();

    public GameObject GetNext(Settings.Projectile.Type type, Vector3 position, Quaternion rotation, float speedBoost,
        Monster monster)
    {
        var pool = GetPool(_poolDict, type);

        var projectile = pool.Count > 0 ? pool.Pop() : Projectile.Create(type);

        projectile.Reset(position, rotation, speedBoost, monster.gameObject);

        Show(projectile);

        return projectile.gameObject;
    }

    private static Stack<Projectile> GetPool(Dictionary<Settings.Projectile.Type, Stack<Projectile>> poolDict,
        Settings.Projectile.Type type)
    {
        Stack<Projectile> pool;
        poolDict.TryGetValue(type, out pool);

        if (pool == null)
        {
            pool = new Stack<Projectile>();
            poolDict.Add(type, pool);
        }

        return pool;
    }

    private static void Show(Component projectile)
    {
        projectile.gameObject.SetActive(true);
    }

    public void Hide(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        GetPool(_poolDict, projectile.GetProjectileType()).Push(projectile);
    }
}
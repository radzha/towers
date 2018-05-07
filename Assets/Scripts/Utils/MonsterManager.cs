using System;
using System.Collections.Generic;
using Progress;
using UnityEngine;

public class MonsterManager : SingletonAuto<MonsterManager>
{
    private readonly Stack<Monster> _pool = new Stack<Monster>();
    private readonly HashSet<Monster> _activeMonsters = new HashSet<Monster>();

    private Transform _monstersParent;

    public void CreateMonster(Vector3 initPositon, Vector3 targetPosition)
    {
        GetNextMonster(initPositon, targetPosition);
    }

    private Monster GetNextMonster(Vector3 initPositon, Vector3 targetPosition)
    {
        Monster monster;

        if (_pool.Count > 0)
        {
            monster = _pool.Pop();
            monster.Reset();

            var monsterObject = monster.gameObject;
            monsterObject.transform.position = initPositon;
            monsterObject.tag = "Explodable";
            
            var meshRenderer = monsterObject.GetComponent<MeshRenderer>();
            monster.TargetPosition = targetPosition;
            meshRenderer.material.color = monster.GetColor();
        }
        else
        {
            var monsterObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);

            SetParent(monsterObject.transform);

            var meshRenderer = monsterObject.GetComponent<MeshRenderer>();

            var body = monsterObject.AddComponent<Rigidbody>();
            body.useGravity = false;
            body.angularDrag = float.PositiveInfinity;
            body.drag = float.PositiveInfinity;
            body.constraints = RigidbodyConstraints.FreezePositionY
                               | RigidbodyConstraints.FreezeRotationX
                               | RigidbodyConstraints.FreezeRotationZ;

            monsterObject.transform.position = initPositon;
            monster = monsterObject.AddComponent<Monster>();
            monster.TargetPosition = targetPosition;
            meshRenderer.material.color = monster.GetColor();
        }

        ShowMonster(monster);

        return monster;
    }

    private void SetParent(Transform monster)
    {
        if (_monstersParent == null)
        {
            var o = GameObject.Find("Monsters");
            if (o != null)
            {
                _monstersParent = o.transform;
            }
        }

        monster.transform.SetParent(_monstersParent);
    }

    public void HideMonster(Monster monster)
    {
        _activeMonsters.Remove(monster);
        monster.gameObject.SetActive(false);
        _pool.Push(monster);
    }

    private void ShowMonster(Monster monster)
    {
        monster.gameObject.SetActive(true);
        _activeMonsters.Add(monster);
    }

    public HashSet<Monster> GetActiveMonsters()
    {
        return _activeMonsters;
    }
}
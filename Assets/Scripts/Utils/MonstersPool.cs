using System.Collections.Generic;
using Progress;
using UnityEngine;

public class MonstersPool : SingletonAuto<MonstersPool>
{
    private readonly Stack<Monster> _pool = new Stack<Monster>();

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
            var meshRenderer = monsterObject.GetComponent<MeshRenderer>();
            monsterObject.transform.position = initPositon;
            monster.TargetPosition = targetPosition;
            meshRenderer.material.color = monster.GetColor();

            monster.gameObject.SetActive(true);

            "Monster from pool".CLogBlue(monster.GetHashCode());
        }
        else
        {
            var monsterObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            var meshRenderer = monsterObject.GetComponent<MeshRenderer>();
            var body = monsterObject.AddComponent<Rigidbody>();
            body.useGravity = false;
            monsterObject.transform.position = initPositon;
            monster = monsterObject.AddComponent<Monster>();
            monster.TargetPosition = targetPosition;
            meshRenderer.material.color = monster.GetColor();

            "Monster new".CLog(monster.GetHashCode());
        }

        return monster;
    }

    public void HideMonster(Monster monster)
    {
        "Monster was hidden".CLogRed(monster.GetHashCode());
        
        monster.gameObject.SetActive(false);
        _pool.Push(monster);
    }
}
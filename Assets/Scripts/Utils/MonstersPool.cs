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
        }

        return monster;
    }
}
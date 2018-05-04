using System.Collections.Generic;
using Progress;
using UnityEngine;

public class MonstersPool : Singleton<MonstersPool>
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
            var body = monsterObject.AddComponent<Rigidbody>();
            body.useGravity = false;
            monsterObject.transform.position = initPositon;
            monster = monsterObject.AddComponent<Monster>();
            monster.TargetPosition = targetPosition;
        }

        return monster;
    }
}

// Родитель синглтонов.
public class Singleton<T>:MonoBehaviour where T : class {

    public static T Instance {
        get;
        protected set;
    }

    public Singleton() {
        Instance = this as T;
    }

}
//public class Singleton<T> : MonoBehaviour where T : new()
//{
//    private static T _instance;
//
//    public static T Instance
//    {
//        get
//        {
//            if (_instance == null)
//            {
//                _instance = new T();
//            }
//
//            Debug.Log("new: " + _instance);
//            return _instance;
//        }
//    }
//}
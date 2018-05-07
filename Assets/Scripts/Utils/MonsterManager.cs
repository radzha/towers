using System.Collections.Generic;
using Progress;
using UnityEngine;

/// <summary>
/// Менеджер, управляющий пулом монстров.
/// </summary>
public class MonsterManager : SingletonAuto<MonsterManager>
{
    private readonly Stack<Monster> _pool = new Stack<Monster>();
    private readonly HashSet<Monster> _activeMonsters = new HashSet<Monster>();

    private Transform _monstersParent;
    private GameObject _getParentObject;

    private GameObject GetParentObject
    {
        get
        {
            if (_getParentObject == null)
            {
                _getParentObject = GameObject.Find("Monsters");
            }

            return _getParentObject;
        }
    }

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
            InitMonster(monster, initPositon, targetPosition);
        }
        else
        {
            var monsterObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);

            monsterObject.tag = Constants.ExplodableTag;

            SetParent(monsterObject.transform);

            AddRigidBody(monsterObject);

            monster = monsterObject.AddComponent<Monster>();
            InitMonster(monster, initPositon, targetPosition);
        }

        ShowMonster(monster);

        return monster;
    }

    private static void InitMonster(Monster monster, Vector3 initPositon, Vector3 targetPosition)
    {
        monster.Reset();

        var monsterObject = monster.gameObject;
        monsterObject.transform.position = initPositon;

        var meshRenderer = monsterObject.GetComponent<MeshRenderer>();
        monster.TargetPosition = targetPosition;
        meshRenderer.material.color = monster.GetColor();
    }

    private static void AddRigidBody(GameObject monsterObject)
    {
        var body = monsterObject.AddComponent<Rigidbody>();
        body.useGravity = false;
        body.angularDrag = float.PositiveInfinity;
        body.drag = float.PositiveInfinity;
        body.constraints = RigidbodyConstraints.FreezePositionY
                           | RigidbodyConstraints.FreezeRotationX
                           | RigidbodyConstraints.FreezeRotationZ;
    }

    private void SetParent(Component monster)
    {
        if (_monstersParent == null)
        {
            var o = GetParentObject;

            if (o != null) _monstersParent = o.transform;
        }

        monster.transform.SetParent(_monstersParent);
    }

    private void ShowMonster(Monster monster)
    {
        monster.gameObject.SetActive(true);
        _activeMonsters.Add(monster);
    }

    public void HideMonster(Monster monster)
    {
        _activeMonsters.Remove(monster);
        monster.gameObject.SetActive(false);
        _pool.Push(monster);
    }

    public HashSet<Monster> GetActiveMonsters()
    {
        return _activeMonsters;
    }
}
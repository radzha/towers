using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _mInterval = 3;

    [SerializeField] private GameObject _mMoveTarget;

    private float _mLastSpawn = -1;

    private void Update()
    {
        if (Time.time > _mLastSpawn + _mInterval)
        {
            var newMonster = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            var r = newMonster.AddComponent<Rigidbody>();
            r.useGravity = false;
            newMonster.transform.position = transform.position;
            var monsterBeh = newMonster.AddComponent<Monster>();
            monsterBeh.MMoveTarget = _mMoveTarget;

            _mLastSpawn = Time.time;
        }
    }
}
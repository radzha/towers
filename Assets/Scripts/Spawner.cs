using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _interval = 3;

    [SerializeField] private Transform _moveTarget;

    private float _lastSpawnTime = -1;

    private void Update()
    {
        if (Time.time > _lastSpawnTime + _interval)
        {
            MonstersPool.Instance.CreateMonster(transform.position, _moveTarget.position);
            _lastSpawnTime = Time.time;
        }
    }
}
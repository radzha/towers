using UnityEngine;

public class CannonTower : MonoBehaviour
{
    [SerializeField] private float _mShootInterval = 0.5f;
    [SerializeField] private float _mRange = 4f;
    [SerializeField] private GameObject _mProjectilePrefab;
    [SerializeField] private Transform _mShootPoint;

    private float m_lastShotTime = -0.5f;

    private void Update()
    {
        if (_mProjectilePrefab == null || _mShootPoint == null)
            return;

        foreach (var monster in FindObjectsOfType<Monster>())
        {
            if (Vector3.Distance(transform.position, monster.transform.position) > _mRange)
                continue;

            if (m_lastShotTime + _mShootInterval > Time.time)
                continue;

            // shot
            Instantiate(_mProjectilePrefab, _mShootPoint.position, _mShootPoint.rotation);

            m_lastShotTime = Time.time;
        }
    }
}
using UnityEngine;

public class SimpleTower : MonoBehaviour
{
    [SerializeField] private float _mShootInterval = 0.5f;
    [SerializeField] private float _mRange = 4f;
    [SerializeField] private GameObject _mProjectilePrefab;

    private float _mLastShotTime = -0.5f;

    private void Update()
    {
        if (_mProjectilePrefab == null) return;

        foreach (var monster in FindObjectsOfType<Monster>())
        {
            if (Vector3.Distance(transform.position, monster.transform.position) > _mRange)
                continue;

            if (_mLastShotTime + _mShootInterval > Time.time)
                continue;

            // shot
            var projectile =
                Instantiate(_mProjectilePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);

            var projectileBeh = projectile.GetComponent<GuidedProjectile>();

            projectileBeh.m_target = monster.gameObject;

            _mLastShotTime = Time.time;
        }
    }
}
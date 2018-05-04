using UnityEngine;

public class CannonTower : Tower
{
    [SerializeField] private Transform _mShootPoint;

    private float _mLastShotTime = -0.5f;

    protected override bool MissingPrefabs()
    {
        return base.MissingPrefabs() || _mShootPoint == null;
    }

    protected override Vector3 GetShootPosition()
    {
        return _mShootPoint.position;
    }

    protected override Quaternion GetShootRotation()
    {
        return _mShootPoint.rotation;
    }
}
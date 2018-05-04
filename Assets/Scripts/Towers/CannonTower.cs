using UnityEngine;

public class CannonTower : Tower
{
    [SerializeField] private Transform _mShootPoint;

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
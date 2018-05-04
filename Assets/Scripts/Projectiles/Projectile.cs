using Progress;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float MSpeed = 0.2f;

    [SerializeField] private int _mDamage = 10;

    private void OnTriggerEnter(Collider other)
    {
        var monster = other.gameObject.GetComponent<Monster>();
        if (monster == null)
            return;

        monster.TakeDamage(_mDamage);
        if (monster.Health() <= 0)
        {
            Destroy(monster.gameObject);
        }

        Destroy(gameObject);
    }
}
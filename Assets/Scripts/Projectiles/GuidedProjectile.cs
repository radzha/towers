using UnityEngine;

public class GuidedProjectile : Projectile
{
    public GameObject MTarget;

    private void Update()
    {
        if (MTarget == null)
        {
            Destroy(gameObject);
            return;
        }

        var translation = MTarget.transform.position - transform.position;
        if (translation.magnitude > MSpeed)
        {
            translation = translation.normalized * MSpeed;
        }

        transform.Translate(translation);
    }
}
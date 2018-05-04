using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject MMoveTarget { get; set; }
    public int Health { get; set; }

    private float Speed = 0.1f;
    private int MaxHealth = 30;
    private float ReachDistance = 0.3f;

    private void Start()
    {
        Health = MaxHealth;
    }

    private void Update()
    {
        if (MMoveTarget == null)
            return;

        if (Vector3.Distance(transform.position, MMoveTarget.transform.position) <= ReachDistance)
        {
            Destroy(gameObject);
            return;
        }

        var translation = MMoveTarget.transform.position - transform.position;
        if (translation.magnitude > Speed)
        {
            translation = translation.normalized * Speed;
        }

        transform.Translate(translation);
    }
}
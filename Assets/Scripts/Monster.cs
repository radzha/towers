using UnityEngine;

public class Monster : MonoBehaviour
{
    public Vector3 TargetPosition { get; set; }
    public int Health { get; set; }

    private float Speed = 0.1f;
    private int MaxHealth = 30;
    private float ReachDistance = 0.3f;

    private bool dead { get; set; }

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, TargetPosition) <= ReachDistance)
        {
            Destroy(gameObject);
            return;
        }

        var translation = TargetPosition - transform.position;
        if (translation.magnitude > Speed)
        {
            translation = translation.normalized * Speed;
        }

        transform.Translate(translation);
    }

    public void Reset()
    {
        Health = MaxHealth;
    }
}
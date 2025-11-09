using UnityEngine;

public class EnemyTouchDamage : MonoBehaviour
{
    public int damage = 1;
    public float reach = 1.0f;
    public float cooldown = 0.8f;
    Transform target;
    PlayerHealth targetHealth;
    float nextTime;

    void Start()
    {
        var ph = FindAnyObjectByType<PlayerHealth>();
        if (ph) { targetHealth = ph; target = ph.transform; }
    }

    void Update()
    {
        if (!targetHealth || Time.time < nextTime) return;
        var a = transform.position; a.y = 0;
        var b = target.position; b.y = 0;
        if (Vector3.Distance(a, b) <= reach)
        {
            targetHealth.Damage(damage);
            nextTime = Time.time + cooldown;
        }
    }
}

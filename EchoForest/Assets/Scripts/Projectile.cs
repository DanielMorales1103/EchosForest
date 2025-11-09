using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 3f;
    public LayerMask hitMask;
    Vector3 dir;
    float speed;
    bool launched;

    public void Launch(Vector3 direction, float s)
    {
        dir = direction.normalized;
        speed = s;
        launched = true;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (!launched) return;
        transform.position += dir * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & hitMask) == 0) return;
        var k = other.GetComponentInParent<EnemyKillable>();
        if (k) k.Kill();
        Destroy(gameObject);
    }
}

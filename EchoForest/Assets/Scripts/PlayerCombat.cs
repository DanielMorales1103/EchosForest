using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public KeyCode meleeKey = KeyCode.F;
    public float meleeRange = 1.6f;
    public float meleeRadius = 0.9f;
    public float meleeCooldown = 0.35f;
    public LayerMask enemyMask;

    public Transform firePoint;
    public GameObject projectilePrefab;
    public float projectileSpeed = 18f;
    public float shootCooldown = 0.25f;

    float nextMeleeTime;
    float nextShootTime;

    void Update()
    {
        if (Input.GetKeyDown(meleeKey) && Time.time >= nextMeleeTime)
        {
            DoMelee();
            nextMeleeTime = Time.time + meleeCooldown;
        }

        if (Input.GetMouseButtonDown(0) && Time.time >= nextShootTime)
        {
            DoShoot();
            nextShootTime = Time.time + shootCooldown;
        }
    }

    void DoMelee()
    {
        Vector3 center = transform.position + transform.forward * meleeRange;
        var hits = Physics.OverlapSphere(center, meleeRadius, enemyMask, QueryTriggerInteraction.Collide);
        for (int i = 0; i < hits.Length; i++)
        {
            var h = hits[i].GetComponentInParent<EnemyKillable>();
            if (h) h.Kill();
            else Destroy(hits[i].attachedRigidbody ? hits[i].attachedRigidbody.gameObject : hits[i].gameObject);
        }
    }

    void DoShoot()
    {
        if (!projectilePrefab) return;
        Vector3 origin = firePoint ? firePoint.position : transform.position + Vector3.up * 1.5f;
        Vector3 dir = transform.forward.normalized;
        var go = Instantiate(projectilePrefab, origin, Quaternion.LookRotation(dir));
        var proj = go.GetComponent<Projectile>();
        if (proj) proj.Launch(dir, projectileSpeed);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var pos = transform ? transform.position : Vector3.zero;
        Gizmos.DrawWireSphere(pos + (transform ? transform.forward : Vector3.forward) * meleeRange, meleeRadius);
    }
}

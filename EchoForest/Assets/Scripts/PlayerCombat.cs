using UnityEngine;
using System.Collections;
using StarterAssets;

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
    public float shootCooldown = 5.0f;

    public float meleeLockTime = 1f;
    public float shootLockTime = 4f;

    float nextMeleeTime;
    float nextShootTime;

    private Animator animator;
    private ThirdPersonController controller;
    private StarterAssetsInputs inputs;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        controller = GetComponent<ThirdPersonController>();
        if (!controller) controller = GetComponentInParent<ThirdPersonController>();

        inputs = GetComponent<StarterAssetsInputs>();
        if (!inputs) inputs = GetComponentInParent<StarterAssetsInputs>();
    }

    void Update()
    {
        if (Input.GetKeyDown(meleeKey) && Time.time >= nextMeleeTime)
        {
            
            nextMeleeTime = Time.time + meleeCooldown;
            if (animator) animator.SetTrigger("Punch");
            StartCoroutine(LockMovement(meleeLockTime));
            DoMelee();
            //animator.SetTrigger("Punch");
        }

        if (Input.GetMouseButtonDown(0) && Time.time >= nextShootTime)
        {
            nextShootTime = Time.time + shootCooldown;
            if (animator) animator.SetTrigger("Shoot");
            StartCoroutine(LockMovement(shootLockTime));
            StartCoroutine(ShootDelayed());
        }
    }

    IEnumerator ShootDelayed()
    {
        yield return new WaitForSeconds(1f);
        DoShoot();                              
    }

    IEnumerator LockMovement(float duration)
    {
        // Desactivamos movimiento
        if (controller != null)
            controller.enabled = false;

        if (inputs != null)
        {
            inputs.move = Vector2.zero;
            inputs.jump = false;
            inputs.sprint = false;
        }

        yield return new WaitForSeconds(duration);

        // Volvemos a activarlo
        if (controller != null)
            controller.enabled = true;
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

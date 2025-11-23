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

    private Camera mainCam;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        controller = GetComponent<ThirdPersonController>();
        if (!controller) controller = GetComponentInParent<ThirdPersonController>();

        inputs = GetComponent<StarterAssetsInputs>();
        if (!inputs) inputs = GetComponentInParent<StarterAssetsInputs>();

        mainCam = Camera.main;
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
        if (controller != null)
            controller.enabled = false;

        if (inputs != null)
        {
            inputs.move = Vector2.zero;
            inputs.jump = false;
            inputs.sprint = false;
        }

        yield return new WaitForSeconds(duration);

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

        Vector3 origin = firePoint
            ? firePoint.position
            : transform.position + Vector3.up * 1.5f;

        if (mainCam == null)
        {
            Vector3 fallbackDir = transform.forward.normalized;
            var goFallback = Instantiate(projectilePrefab, origin, Quaternion.LookRotation(fallbackDir));
            var projFallback = goFallback.GetComponent<Projectile>();
            if (projFallback) projFallback.Launch(fallbackDir, projectileSpeed);
            return;
        }

        Vector3 camPos = mainCam.transform.position;
        Vector3 camForward = mainCam.transform.forward;

        Ray ray = new Ray(camPos, camForward);
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, ~0, QueryTriggerInteraction.Ignore))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = camPos + camForward * 1000f;
        }

        Vector3 dirFromCamera = (targetPoint - origin).normalized;

        float dot = Vector3.Dot(transform.forward, dirFromCamera);

        float t = Mathf.Clamp01(dot);  

        Vector3 finalDir = Vector3.Slerp(transform.forward, dirFromCamera, t).normalized;

        var go = Instantiate(projectilePrefab, origin, Quaternion.LookRotation(finalDir));
        var proj = go.GetComponent<Projectile>();
        if (proj) proj.Launch(finalDir, projectileSpeed);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var pos = transform ? transform.position : Vector3.zero;
        Gizmos.DrawWireSphere(pos + (transform ? transform.forward : Vector3.forward) * meleeRange, meleeRadius);
    }
}

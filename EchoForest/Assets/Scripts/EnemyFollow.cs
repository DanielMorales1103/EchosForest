using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 1f;
    public float turnSpeed = 10f;
    public float stopDistance = 1.0f;
    public float targetHeight = 1.0f;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Si no se asignó target desde el spawner, lo buscamos por tag
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }
    }

    void FixedUpdate()
    {
        if (!target || !rb) return;

        Vector3 targetPos = target.position;
        targetPos.y += targetHeight;   

        Vector3 toTarget = targetPos - transform.position;
        float dist = toTarget.magnitude;
        if (dist <= stopDistance) return;

        Vector3 dir = toTarget.normalized;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, turnSpeed * Time.fixedDeltaTime);

        Vector3 newPos = transform.position + dir * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }

}

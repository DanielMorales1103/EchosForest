using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 1f;
    [SerializeField] float turnSpeed = 10f;
    [SerializeField] float stopDistance = 0.0f;

    [SerializeField] float heightOffset = 1.0f;
    [SerializeField] float verticalSpeed = 1f;
    [SerializeField] bool useControllerHeight = true;
    [SerializeField] float heightRatio = 0.6f;

    Transform tgt;
    float targetY;

    void Awake()
    {
        if (!target)
        {
            var inv = FindAnyObjectByType<Inventory>();
            if (inv) tgt = inv.transform;
        }
        else tgt = target;

        if (TryGetComponent<Rigidbody>(out var rb)) rb.useGravity = false;
    }

    void Update()
    {
        if (!tgt) return;

        var pos = transform.position;
        var tpos = tgt.position;

        if (useControllerHeight && tgt.TryGetComponent<CharacterController>(out var cc))
            targetY = tpos.y + cc.height * heightRatio;
        else
            targetY = tpos.y + heightOffset;

        var flatDir = new Vector3(tpos.x - pos.x, 0f, tpos.z - pos.z);
        var dist = flatDir.magnitude;
        if (dist > stopDistance)
        {
            var moveDir = flatDir.normalized;
            var desiredRot = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, turnSpeed * Time.deltaTime);
            transform.position += moveDir * speed * Time.deltaTime;
        }

        var p = transform.position;
        p.y = Mathf.MoveTowards(p.y, targetY, verticalSpeed * Time.deltaTime);
        transform.position = p;
    }

    public void SetTarget(Transform t) => tgt = t;
}

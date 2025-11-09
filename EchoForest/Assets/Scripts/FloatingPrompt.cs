using UnityEngine;
using TMPro;

public class FloatingPrompt : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private Vector3 localOffset = new Vector3(0, 1.8f, 0);
    private Camera cam;
    private Transform target;

    void Awake()
    {
        cam = Camera.main;
        if (!label) label = GetComponentInChildren<TMP_Text>(true);
        gameObject.SetActive(false);
    }

    public void Attach(Transform t)
    {
        target = t;
        transform.SetParent(target, false);
        transform.localPosition = localOffset;
        transform.localRotation = Quaternion.identity;
        //transform.localScale = Vector3.one * 0.01f;
    }

    public void Show(string text) { if (label) label.text = text; gameObject.SetActive(true); }
    public void Hide() => gameObject.SetActive(false);

    void LateUpdate()
    {
        if (!cam) cam = Camera.main;
        if (!target) return;

        Vector3 toCam = (transform.position - cam.transform.position);
        if (toCam.sqrMagnitude > 0.0001f)
            transform.rotation = Quaternion.LookRotation(toCam, Vector3.up);
    }
}

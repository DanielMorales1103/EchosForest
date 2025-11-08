using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float range = 1.5f;
    [SerializeField] private LayerMask interactMask;
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private float startOffset = 0.4f;

    private IInteractable current;
    private FloatingPrompt currentPrompt;
    private FloatingPrompt lastPrompt;

    void Awake()
    {
        if (!rayOrigin && Camera.main) rayOrigin = Camera.main.transform;
    }

    void Update()
    {
        UpdateCurrent();
        if (current != null && Input.GetKeyDown(KeyCode.E))
            current.Interact(gameObject);
    }

    void UpdateCurrent()
    {
        current = null;
        currentPrompt = null;

        Vector3 origin = rayOrigin.position + rayOrigin.forward * startOffset;
        var ray = new Ray(origin, rayOrigin.forward);

        Debug.DrawRay(origin, rayOrigin.forward * range, Color.cyan);  // Para ver el rayo

        if (Physics.Raycast(ray, out var hit, range, interactMask, QueryTriggerInteraction.Collide))
        {
            Debug.Log("Hit detected: " + hit.collider.name);  // Agregado para saber si el rayo está detectando el objeto

            current = hit.collider.GetComponentInParent<IInteractable>();
            if (current != null)
            {
                var root = ((MonoBehaviour)current).transform;
                currentPrompt = root.GetComponentInChildren<FloatingPrompt>(true);
                if (currentPrompt)
                {
                    currentPrompt.Attach(root);
                }
            }
        }
        else
        {
            Debug.Log("No hit detected.");
        }

        if (lastPrompt && lastPrompt != currentPrompt)
            lastPrompt.Hide();

        if (current != null && currentPrompt != null)
        {
            currentPrompt.Show(current.PromptText);
            lastPrompt = currentPrompt;
        }
        else
        {
            if (lastPrompt) lastPrompt.Hide();
            lastPrompt = null;
        }
    }

}

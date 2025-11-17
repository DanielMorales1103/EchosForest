using UnityEngine;
using StarterAssets;
using System.Collections;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float range = 1.5f;
    [SerializeField] private LayerMask interactMask;
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private float startOffset = 0.4f;

    public float interactLockTime = 3.5f;

    private IInteractable current;
    private FloatingPrompt currentPrompt;
    private FloatingPrompt lastPrompt;

    private Animator animator;
    private ThirdPersonController controller;
    private StarterAssetsInputs inputs;

    void Awake()
    {
        if (!rayOrigin && Camera.main) rayOrigin = Camera.main.transform;
        animator = GetComponentInChildren<Animator>();

        controller = GetComponent<ThirdPersonController>();
        if (!controller) controller = GetComponentInParent<ThirdPersonController>();

        inputs = GetComponent<StarterAssetsInputs>();
        if (!inputs) inputs = GetComponentInParent<StarterAssetsInputs>();
    }

    void Update()
    {
        UpdateCurrent();
        if (current != null && Input.GetKeyDown(KeyCode.E))
        {
            PlayInteractAnimation();
            current.Interact(gameObject);                     
        }
    }

    void PlayInteractAnimation()
    {
        if (!animator) return;

        var mb = current as MonoBehaviour;
        if (!mb) return;

        StartCoroutine(LockMovement(interactLockTime));

        if (mb.CompareTag("Totem"))
        {
            Debug.Log("Playing get animation");
            animator.SetTrigger("get");
        }
        else if (mb.CompareTag("Altar"))
        {
            animator.SetTrigger("Use");
        }
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


    void UpdateCurrent()
    {
        current = null;
        currentPrompt = null;

        Vector3 origin = rayOrigin.position + rayOrigin.forward * startOffset;
        var ray = new Ray(origin, rayOrigin.forward);

        Debug.DrawRay(origin, rayOrigin.forward * range, Color.cyan);  

        if (Physics.Raycast(ray, out var hit, range, interactMask, QueryTriggerInteraction.Collide))
        {

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

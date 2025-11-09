using UnityEngine;
using UnityEngine.Events;

public class Altar : MonoBehaviour, IInteractable
{
    [SerializeField] TotemDefinition requiredTotem;
    [SerializeField] UnityEvent onPlaced;
    bool completed;

    public string PromptText => "[E] Interact";

    public void Interact(GameObject by)
    {
        if (completed) return;
        var inv = by.GetComponent<Inventory>();
        if (!inv || !requiredTotem) return;
        if (!inv.Has(requiredTotem)) return;

        if (inv.Remove(requiredTotem, 1))
        {
            completed = true;
            onPlaced?.Invoke();
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemPickup : MonoBehaviour, IInteractable
{
    public ItemDefinition itemDef;
    public int amount = 1;

    public string PromptText => "[E] Interact";

    void Reset()
    {
        var c = GetComponent<Collider>();
        c.isTrigger = true;
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    public void Interact(GameObject by)
    {
        var inv = by.GetComponent<Inventory>();
        if (!inv || !itemDef) return;

        if (inv.Add(itemDef, amount))
        {
            Destroy(gameObject);
        }
    }
}

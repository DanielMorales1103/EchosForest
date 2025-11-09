using System.Linq;
using UnityEngine;

public class InventoryHUD : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] Transform container;
    [SerializeField] UIItemSlot slotPrefab;

    void Start()
    {
        if (!inventory) inventory = FindAnyObjectByType<Inventory>();
        if (inventory) inventory.OnChanged += Refresh;
        Refresh();
    }

    void OnDestroy()
    {
        if (inventory) inventory.OnChanged -= Refresh;
    }

    void Refresh()
    {
        foreach (Transform c in container) Destroy(c.gameObject);
        if (!inventory) return;
        foreach (var s in inventory.slots.Where(s => s.amount > 0))
        {
            var slot = Instantiate(slotPrefab, container);
            slot.Set(s.def.icon, s.amount);
        }
    }
}

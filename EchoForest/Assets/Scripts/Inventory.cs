using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ItemStack
{
    public ItemDefinition def;
    public int amount = 1;
}

public class Inventory : MonoBehaviour
{
    [SerializeField] private int maxSlots = 6;
    public List<ItemStack> slots = new();

    public System.Action OnChanged;

    public bool CanAdd(ItemDefinition def, int amount = 1)
    {
        if (def == null) return false;
        if (def.stackable)
        {
            var stack = slots.FirstOrDefault(s => s.def == def);
            if (stack != null) return stack.amount + amount <= def.maxStack;
        }
        return slots.Count < maxSlots;
    }

    public bool Add(ItemDefinition def, int amount = 1)
    {
        if (!CanAdd(def, amount)) return false;

        if (def.stackable)
        {
            var stack = slots.FirstOrDefault(s => s.def == def);
            if (stack != null) stack.amount += amount;
            else slots.Add(new ItemStack { def = def, amount = amount });
        }
        else
        {
            slots.Add(new ItemStack { def = def, amount = 1 });
        }

        OnChanged?.Invoke();
        Debug.Log($"[Inventory] Add: {def.displayName} x{amount}");
        return true;
    }

    public bool Remove(ItemDefinition def, int amount = 1)
    {
        var stack = slots.FirstOrDefault(s => s.def == def);
        if (stack == null || stack.amount < amount) return false;

        stack.amount -= amount;
        if (stack.amount <= 0) slots.Remove(stack);
        OnChanged?.Invoke();
        return true;
    }

    public bool Has(ItemDefinition def) => slots.Any(s => s.def == def && s.amount > 0);
}

using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public int max = 5;
    public int current;
    public UnityEvent onDamaged;
    public UnityEvent onDied;

    void Awake()
    {
        current = max;
    }

    public void Damage(int amount)
    {
        if (current <= 0) return;
        current = Mathf.Max(0, current - amount);
        onDamaged?.Invoke();
        if (current == 0) onDied?.Invoke();
    }

    public void Heal(int amount)
    {
        if (current <= 0) return;
        current = Mathf.Min(max, current + amount);
    }
}

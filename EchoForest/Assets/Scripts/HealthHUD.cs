using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Slider slider;
    public Image fillImage;

    void Start()
    {
        if (!playerHealth) playerHealth = FindAnyObjectByType<PlayerHealth>();
        slider.minValue = 0;
        slider.maxValue = playerHealth.max;
        slider.value = playerHealth.current;
        playerHealth.onDamaged.AddListener(Refresh);
        playerHealth.onDied.AddListener(Refresh);
        Refresh();
    }

    void Refresh()
    {
        slider.value = playerHealth.current;
    }
}

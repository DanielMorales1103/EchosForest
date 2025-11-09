using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIItemSlot : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text count;

    public void Set(Sprite sprite, int amount)
    {
        icon.sprite = sprite;
        count.text = amount > 1 ? $"x{amount}" : "";
    }
}

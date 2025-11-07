using UnityEngine;

public enum ItemKind { Totem, KeyItem, Resource }
[CreateAssetMenu(fileName = "ItemDefinition", menuName = "Game/Item Definition")]
public class ItemDefinition : ScriptableObject
{
    [Header("Identity")]
    public string itemId;
    public string displayName;
    public ItemKind kind = ItemKind.Totem;

    [Header("Presentation")]
    public Sprite icon;
    public GameObject worldPrefab;

    [Header("Rules")]
    public bool stackable = false;
    public int maxStack = 1;
}

using UnityEngine;
[CreateAssetMenu(fileName = "TotemDefinition", menuName = "Game/Totem Definition")]
public class TotemDefinition : ItemDefinition
{
    public enum TotemType { Agua, Fuego, Viento, Tierra }
    public TotemType totemType = TotemType.Agua;

    [Tooltip("Id del altar que acepta este totem")]
    public string altarIdRequired;
}

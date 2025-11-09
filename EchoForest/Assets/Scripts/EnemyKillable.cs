using UnityEngine;

public class EnemyKillable : MonoBehaviour
{
    public void Kill()
    {
        Destroy(gameObject);
    }
}

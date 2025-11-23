using UnityEngine;

public class EnemyKillable : MonoBehaviour
{
    bool dead;
    public void Kill()
    {
        if (dead) return;
        dead = true;
        if (GameManagerVictory.Instance != null)
            GameManagerVictory.Instance.RegisterEnemyDeath();
        Destroy(gameObject);
    }
}

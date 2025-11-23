using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerVictory : MonoBehaviour
{
    public static GameManagerVictory Instance { get; private set; }

    [Header("Objetivo")]
    public int totalAltars = 2;      // tú dices cuántos altares/tótems hay en el nivel

    int completedAltars = 0;
    int aliveEnemies = 0;
    bool victoryTriggered = false;

    [Header("Escena de victoria")]
    public string victorySceneName = "Victory";  // pon aquí el nombre de tu escena de win

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RegisterAltarCompleted()
    {
        completedAltars++;
    }

    public void RegisterEnemySpawn(int amount)
    {
        aliveEnemies += amount;
    }

    public void RegisterEnemyDeath()
    {
        aliveEnemies = Mathf.Max(0, aliveEnemies - 1);
        CheckVictory();
    }

    void CheckVictory()
    {
        if (victoryTriggered) return;

        if (completedAltars >= totalAltars && aliveEnemies <= 0)
        {
            victoryTriggered = true;
            Debug.Log("VICTORY!");

            if (!string.IsNullOrEmpty(victorySceneName))
            {
                SceneManager.LoadScene(victorySceneName);
            }
        }
    }
}

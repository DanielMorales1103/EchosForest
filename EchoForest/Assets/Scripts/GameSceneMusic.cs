using UnityEngine;

public class GameSceneMusic : MonoBehaviour
{
    void Start()
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PlayGameMusic();
        }
    }
}

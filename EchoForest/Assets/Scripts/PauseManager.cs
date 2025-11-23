using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject pauseCanvas;
    public string mainMenuSceneName = "Menu";

    [Header("Components to disable on pause")]
    public MonoBehaviour[] componentsToDisableOnPause;

    public static bool IsGamePaused { get; private set; } = false;

    void Awake()
    {
        Time.timeScale = 1f;
        IsGamePaused = false;

        if (pauseCanvas != null)
            pauseCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (IsGamePaused)
            ResumeGame();
        else
            PauseGame();
    }

    void PauseGame()
    {
        IsGamePaused = true;

        if (pauseCanvas != null)
            pauseCanvas.SetActive(true);

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        foreach (var comp in componentsToDisableOnPause)
        {
            if (comp != null)
                comp.enabled = false;
        }
    }

    public void ResumeGame()
    {
        if (!IsGamePaused) return;

        IsGamePaused = false;

        if (pauseCanvas != null)
            pauseCanvas.SetActive(false);

        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        foreach (var comp in componentsToDisableOnPause)
        {
            if (comp != null)
                comp.enabled = true;
        }
    }

    public void GoToMainMenu()
    {
        IsGamePaused = false;
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        foreach (var comp in componentsToDisableOnPause)
        {
            if (comp != null)
                comp.enabled = true;
        }

        SceneManager.LoadScene(mainMenuSceneName);
    }
    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void OnDisable()
    {
        IsGamePaused = false;
        Time.timeScale = 1f;
    }
}

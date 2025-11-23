using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "Game";
    public string instructionsSceneName = "Instructions";
    public string ControlsSceneName = "Controls";
    public string MenuSceneName = "Menu";

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void GoingControls()
    {
        SceneManager.LoadScene(instructionsSceneName);
    }

    public void GoingInstructions()
    {
        SceneManager.LoadScene(instructionsSceneName);
    }

    public void GoingMenu()
    {
        SceneManager.LoadScene(MenuSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenuPanel; // Reference to the pause menu UI panel

    private bool isPaused; // Tracks whether the game is currently paused

    private void Start()
    {
        // Ensure the game starts unpaused
        SetPauseState(false);
    }

    // Pauses the game and shows the pause menu
    public void PauseGame()
    {
        SetPauseState(true);
        AdManager.instance.ShowAd();
    }

    // Resumes the game and hides the pause menu
    public void ResumeGame() => SetPauseState(false);

    // Sets the pause state of the game
    private void SetPauseState(bool pause)
    {
        AdManager.instance.ShowAd();
        isPaused = pause;
        Time.timeScale = pause ? 0f : 1f; // Stop or resume game time
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(pause); // Show/hide pause menu
    }

    // Loads the Main Menu scene
    public void LoadMainMenu()
    {
        SetPauseState(false); // Ensure game is unpaused before switching scenes
        SceneManager.LoadScene("MainMenu");
    }

    // Quits the game application (works only in builds, not in editor)
    public void QuitGame()
    {
        SetPauseState(false); // Ensure game is unpaused before quitting
        Application.Quit();
    }

}

using UnityEngine;                  
using UnityEngine.SceneManagement;  
public class MainMenuManager : MonoBehaviour
{
    // Loads the scene named "Game"
    public void StartGame() => SceneManager.LoadScene("Game");
    // Loads the scene named "Credits"
    public void Credits() => SceneManager.LoadScene("Credits");
    // Quits the application (only works in a built game, not in the editor)
    public void ExitGame() => Application.Quit();
}


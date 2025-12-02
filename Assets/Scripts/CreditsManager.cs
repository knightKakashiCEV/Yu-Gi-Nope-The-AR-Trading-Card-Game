using UnityEngine;                  
using UnityEngine.SceneManagement;  
public class CreditsManager : MonoBehaviour
{
   
    // Loads the scene named "MainMenu"
    public void LoadMainMenu() => SceneManager.LoadScene("MainMenu");
    // Quits the application (only works in a built game, not in the editor)
    public void ExitGame() => Application.Quit();
}
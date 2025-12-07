using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LPManager : MonoBehaviour
{
    [SerializeField] PhaseManager phasemanger;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI player1LPTxT;
    [SerializeField] TextMeshProUGUI player2LPTxT;
    [SerializeField] GameObject winPanel;
    [SerializeField] TextMeshProUGUI winText;

    [Header("Settings")]
    [SerializeField] int startingLP = 4000;

    int player1LP;
    int player2LP;
    bool gameOver;

    public bool IsGameOver => gameOver;

    private void Start()
    {
        player1LP = startingLP;
        player2LP = startingLP;
        gameOver = false;

        if (winPanel != null)
            winPanel.SetActive(false);

        UpdateUI();
    }

    // Update the LP
    private void UpdateUI()
    {
        if (player1LPTxT != null)
            player1LPTxT.text = player1LP.ToString();

        if (player2LPTxT != null)
            player2LPTxT.text = player2LP.ToString();
    }

    // Make the respective player take damage
    public void DamageCurrentPlayer(int amount)
    {
        if (gameOver || phasemanger == null) return;

        if (phasemanger.CurrentPlayer == 1)
            player1LP = Mathf.Max(0, player1LP - amount);
        else
            player2LP = Mathf.Max(0, player2LP - amount);

        UpdateUI();
        CheckWin();
    }

    // Make the opponent player take damage
    public void DamageOpponent(int amount)
    {
        if (gameOver || phasemanger == null) return;

        if (phasemanger.CurrentPlayer == 1)
            player2LP = Mathf.Max(0, player2LP - amount);
        else
            player1LP = Mathf.Max(0, player1LP - amount);

        UpdateUI();
        CheckWin();
    }

    // Check if anyone has won
    private void CheckWin()
    {
        if (player1LP <= 0 && player2LP <= 0)
        {
            ShowResult("Empate!");
            gameOver = true;
            AdManager.instance.ShowAd();
        }
        else if (player1LP <= 0)
        {
            ShowResult("El Jugador 2 Ha Ganado!");
            gameOver = true;
            AdManager.instance.ShowAd();
        }
        else if (player2LP <= 0)
        {
            ShowResult("El Jugador 1 Ha Ganado!");
            gameOver = true;
            AdManager.instance.ShowAd();
        }
    }

    // Show the result
    private void ShowResult(string message)
    {
        if (winPanel != null)
            winPanel.SetActive(true);

        if (winText != null)
            winText.text = message;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}

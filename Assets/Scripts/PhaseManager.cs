using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum Phase
{
    Draw,
    Standby,
    Main1,
    Battle,
    Main2,
    End
}

public class PhaseManager : MonoBehaviour
{
    [Header("UI References")]
    public Button[] phaseButtons;          // Buttons representing each phase

    [Header("Game State")]
    public Phase currentPhase = Phase.Draw; // Current phase of the game
    public int currentPlayer = 1;           // Tracks which player's turn it is (1 or 2)

    [Header("Phase Durations")]
    public float drawPhaseDuration = 5f;    // Duration of the Draw phase
    public float standbyPhaseDuration = 1f; // Duration of the Standby phase

    private Coroutine phaseCoroutine;       // Reference to the active phase coroutine

    private void Start()
    {
        UpdateButtons();
        phaseCoroutine = StartCoroutine(DrawPhaseCountdown());
    }

    // Advances to the next phase in the cycle
    public void NextPhase()
    {
        // Stop any active countdown coroutine
        if (phaseCoroutine != null)
        {
            StopCoroutine(phaseCoroutine);
            phaseCoroutine = null;
        }

        // Move to the next phase (wrap around if at the end)
        int nextPhaseIndex = ((int)currentPhase + 1) % System.Enum.GetValues(typeof(Phase)).Length;
        currentPhase = (Phase)nextPhaseIndex;

        UpdateButtons();

        // Start countdowns for specific phases
        switch (currentPhase)
        {
            case Phase.Draw:
                phaseCoroutine = StartCoroutine(DrawPhaseCountdown());
                break;
            case Phase.Standby:
                phaseCoroutine = StartCoroutine(StandbyPhaseCountdown());
                break;
        }
    }

    public bool IsMainPhase()
    {
        return currentPhase == Phase.Main1 || currentPhase == Phase.Main2;
    }

    public bool IsBattlePhase()
    {
        return currentPhase == Phase.Battle;
    }


    // Updates the phase buttons' appearance and interactivity
    private void UpdateButtons()
    {
        // Reset all buttons to gray and disable interaction
        foreach (Button btn in phaseButtons)
        {
            Image background = btn.transform.Find("Background").GetComponent<Image>();
            background.color = Color.gray;
            btn.interactable = false;
        }

        // Enable the current phase button
        int index = (int)currentPhase;
        phaseButtons[index].interactable = true;

        // Highlight the current phase button with player-specific color
        Image currentBackground = phaseButtons[index].transform.Find("Background").GetComponent<Image>();
        currentBackground.color = (currentPlayer == 1) ? Color.blue : Color.red;
    }

    // Ends the current player's turn and switches to the other player
    public void EndTurn()
    {
        // Toggle between Player 1 and Player 2
        currentPlayer = (currentPlayer == 1) ? 2 : 1;

        // Reset to Draw phase for the new player
        currentPhase = Phase.Draw;
        UpdateButtons();

        // Start the Draw phase countdown
        phaseCoroutine = StartCoroutine(DrawPhaseCountdown());
    }

    // Handles the countdown for the Draw phase
    private IEnumerator DrawPhaseCountdown()
    {
        float timer = drawPhaseDuration;
        while (timer > 0f)
        {
            Debug.Log($"Draw Phase countdown: {timer} seconds left for Player {currentPlayer}");
            yield return new WaitForSeconds(1f);
            timer -= 1f;
        }
        Debug.Log($"Draw Phase ended automatically for Player {currentPlayer}");
        NextPhase();
    }

    // Handles the countdown for the Standby phase
    private IEnumerator StandbyPhaseCountdown()
    {
        Debug.Log($"Standby Phase for Player {currentPlayer}");
        yield return new WaitForSeconds(standbyPhaseDuration);
        Debug.Log($"Standby Phase ended automatically for Player {currentPlayer}");
        NextPhase();
    }
}

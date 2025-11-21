using UnityEngine;

public class Select : MonoBehaviour
{
    [SerializeField] Defense defensePanel;
    [SerializeField] PhaseManager phaseManager;
    [SerializeField] BattlePhase battlePhase;
    [SerializeField] Attack attackPanel;   // NUEVO

    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            MonsterCard m = hit.collider.GetComponent<MonsterCard>();
            if (m == null) return;

            // MAIN PHASE → defensa
            if (phaseManager.IsMainPhase())
            {
                if (m.CanChangePosition())
                    defensePanel.SetSelected(m);
            }
            // BATTLE PHASE → botón de Attack + selección de objetivo
            else if (phaseManager.IsBattlePhase())
            {
                HandleBattleClick(m);
            }
        }
    }

    void HandleBattleClick(MonsterCard m)
    {
        // Si aún no hemos elegido atacante → mostrar botón de Attack
        if (!battlePhase.HasAttacker())
        {
            if (m.CanBeAttacker())
            {
                attackPanel.SetSelected(m);
            }
        }
        // Si ya hay atacante → este click es el objetivo
        else
        {
            battlePhase.SelectTarget(m);
        }
    }
}

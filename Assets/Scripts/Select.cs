using UnityEngine;

public class Select : MonoBehaviour
{
    [SerializeField] Defense defenseButton;
    [SerializeField] PhaseManager phaseManager;
    [SerializeField] BattlePhase battlePhase;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                MonsterCard m = hit.collider.GetComponent<MonsterCard>();
                if (m == null) return;

                // MAIN PHASE → cambiar a defensa (solo una vez por turno)
                if (phaseManager != null && phaseManager.IsMainPhase())
                {
                    if (m.CanChangePosition())
                        defenseButton.SetSelected(m);
                }
                // BATTLE PHASE → seleccionar atacante / defensor
                else if (phaseManager != null && phaseManager.IsBattlePhase())
                {
                    if (battlePhase != null)
                        battlePhase.SelectForBattle(m);
                }
            }
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class Select : MonoBehaviour
{
    [SerializeField] private Camera rayCamera;
    [SerializeField] private PhaseManager phaseManager;
    [SerializeField] private BattlePhase battlePhase;
    [SerializeField] private Defense defensePanel;
    [SerializeField] private Attack attackPanel;

    void Awake()
    {
        if (rayCamera == null)
            rayCamera = Camera.main;
    }

    void Update()
    {
        // click or tap
        if (!Input.GetMouseButtonDown(0))
            return;

        if (rayCamera == null)
        {
            Debug.LogError("Select: rayCamera is null, assign ARCamera.");
            return;
        }

        // grab the monster and handle respective to the phase
        Ray ray = rayCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            MonsterCard m = hit.collider.GetComponentInParent<MonsterCard>();
            if (m == null)
                return;

            if(m != null) Debug.Log($"Seleccionado: {m.name}");

            if (phaseManager.IsMainPhase())
            {
                HandleMainPhaseClick(m);
            }
            else if (phaseManager.IsBattlePhase())
            {
                HandleBattlePhaseClick(m);
            }
        }
    }

    // Main phase to show defense/attack pos.
    void HandleMainPhaseClick(MonsterCard m)
    {
        if (!m.CanChangePosition() && !m.CanReturnToAttack())
            return;

        defensePanel.Open(m);
    }

    // BattlePhase show Attack and choose attacker/target
    void HandleBattlePhaseClick(MonsterCard m)
    {
        if (!battlePhase.HasAttacker())
        {
            if (!m.CanBeAttacker())
                return;

            attackPanel.Open(m);
        }
        else
        {
            battlePhase.SelectTarget(m);
        }
    }
}

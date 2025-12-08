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

#if UNITY_ANDROID || Unity_IOS

        if(Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began) return;

        Vector2 screenPos = touch.position;

        if (EventSystem.current != null &&
           EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            return;
#else

        
        if (!Input.GetMouseButtonDown(0))
            return;

        Vector2 screenPos = Input.mousePosition;

        if (EventSystem.current != null &&
            EventSystem.current.IsPointerOverGameObject())
            return;

#endif

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

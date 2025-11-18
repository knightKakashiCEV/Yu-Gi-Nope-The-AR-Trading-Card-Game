using UnityEngine;

public class Folder : MonoBehaviour
{
    Defense defenseButton;
    PhaseManager phaseManager;
    BattlePhase battlePhase;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                MonsterCard m = hit.collider.GetComponent<MonsterCard>();

                if (m != null)
                {
                    if (phaseManager.IsMainPhase())
                    {
                        defenseButton.SetSelected(m);
                    }
                    else if (phaseManager.IsBattlePhase())
                    {

                    }
                }
            }
        }
    }
}

using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] BattlePhase battlePhase;

    MonsterCard selected;

    public void SetSelected(MonsterCard m)
    {
        // Re-click same monster → close panel
        if (gameObject.activeSelf && selected == m)
        {
            Cancel();
            return;
        }

        selected = m;
        gameObject.SetActive(true);
    }

    public void PressAttack()
    {
        if (selected != null && battlePhase != null)
        {
            battlePhase.SetAttacker(selected);
        }

        Cancel();
    }

    public void Cancel()
    {
        selected = null;
        gameObject.SetActive(false);
    }
}

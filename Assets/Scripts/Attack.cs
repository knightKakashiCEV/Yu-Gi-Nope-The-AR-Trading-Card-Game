using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private BattlePhase battlePhase;

    private MonsterCard selected;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    // set the panel active
    public void Open(MonsterCard m)
    {
        // Re-click mismo monstruo → cerrar
        if (gameObject.activeSelf && selected == m)
        {
            Cancel();
            return;
        }

        selected = m;
        gameObject.SetActive(true);
    }

    // set the attacker as the current monster
    public void OnAttackButton()
    {
        if (selected != null && battlePhase != null)
            battlePhase.SetAttacker(selected);

        Cancel();
    }

    // cancel the attack
    public void Cancel()
    {
        selected = null;
        gameObject.SetActive(false);
    }
}

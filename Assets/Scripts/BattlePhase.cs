using UnityEngine;

public class BattlePhase : MonoBehaviour
{
    MonsterCard attacker;

    public void SetAttacker(MonsterCard m)
    {
        if (m == null) return;
        if (!m.IsOnField() || !m.CanBeAttacker()) return;

        attacker = m;
        Debug.Log($"Attacker set: {m.name}");
    }

    public bool HasAttacker()
    {
        return attacker != null;
    }

    public void SelectTarget(MonsterCard target)
    {
        if (attacker == null) return;
        if (target == null || !target.IsOnField()) return;
        if (target == attacker) return;

        // Rotar el atacante hacia el objetivo
        Vector3 lookDir = target.transform.position - attacker.transform.position;
        lookDir.y = 0f;
        if (lookDir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir);
            attacker.transform.rotation = targetRot;
        }

        ResolveBattle(attacker, target);
        attacker.MarkAsAttacked();
        attacker = null;
    }

    void ResolveBattle(MonsterCard a, MonsterCard d)
    {
        int atk = a.AttackPoints();
        int defVal = d.IsDefense() ? d.DefensePoints() : d.AttackPoints();

        if (atk > defVal)
        {
            d.SetOnField(false);
        }
        else if (atk < defVal)
        {
            a.SetOnField(false);
        }
        else
        {
            a.SetOnField(false);
            d.SetOnField(false);
        }
    }

    public void ClearAttacker()
    {
        attacker = null;
    }
}

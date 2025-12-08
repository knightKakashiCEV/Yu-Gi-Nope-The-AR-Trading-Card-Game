using UnityEngine;

public class BattlePhase : MonoBehaviour
{
    MonsterCard attacker;

    [SerializeField] LPManager manager;

    // Set which monster is attacking
    public void SetAttacker(MonsterCard m)
    {
        if (m == null) return;
        if (!m.IsOnField() || !m.CanBeAttacker()) return;

        attacker = m;
        Debug.Log($"Attacker set: {m.name}");
    }

    // Check if it does have an attacker selected
    public bool HasAttacker()
    {
        return attacker != null;
    }

    // Select target to attack
    public void SelectTarget(MonsterCard target)
    {
        if (attacker == null) return;
        if (target == null || !target.IsOnField()) return;
        if (target == attacker) return;

        // Rotate attacker towards objective
        Vector3 lookDir = target.transform.position - attacker.transform.position;
        lookDir.y = 0f;
        if (lookDir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir);
            attacker.transform.rotation = targetRot;
        }
        attacker.PlayAttackAnim();

        ResolveBattle(attacker, target);

        attacker.PlayIdle();
        attacker.MarkAsAttacked();
        attacker = null;


    }

    // Resolution of the battle, who wins and loses
    void ResolveBattle(MonsterCard a, MonsterCard d)
    {
        int atkA = a.AttackPoints();
        bool defenderInDefense = d.IsDefense();
        int atkD = d.AttackPoints();
        int defVal = defenderInDefense ? d.DefensePoints() : atkD;
        

        // ATK vs DEF → destruction only
        if (defenderInDefense)
        {
            if (atkA > defVal)
            {
                d.SetOnField(false);
            }
            else if (atkA < defVal)
            {
                a.SetOnField(false);
            }
            else
            {
                a.SetOnField(false);
                d.SetOnField(false);
            }

            return;
        }

        // ATK vs ATK → descrution + damage to the Opponent's LP
        if (atkA > atkD)
        {
            d.SetOnField(false);

            int damage = atkA - atkD;
            if (manager != null)
                manager.DamageOpponent(damage);
        }
        else if (atkA < atkD)
        {
            a.SetOnField(false);

            int damage = atkD - atkA;
            if (manager != null)
                manager.DamageCurrentPlayer(damage);
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

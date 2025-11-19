using UnityEngine;

public class BattlePhase : MonoBehaviour
{
    MonsterCard attacker;

    public void SelectForBattle(MonsterCard m)
    {
        if (m == null || !m.IsOnField())
            return;

        // 1er clic: elegir atacante
        if (attacker == null)
        {
            if (m.CanBeAttacker())
            {
                attacker = m;
                Debug.Log($"Attacker selected: {m.name}");
            }
            return;
        }

        // 2º clic: elegir objetivo y resolver batalla
        if (m != attacker)
        {
            ResolveBattle(attacker, m);
            attacker.MarkAsAttacked();
            attacker = null;
        }
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

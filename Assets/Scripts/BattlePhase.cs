using UnityEngine;

public class BattlePhase : MonoBehaviour
{
    public MonsterCard attacker;

    public void SelectForBattle(MonsterCard m)
    {
        // Elegir atacante
        if (attacker == null)
        {
            if (!m.isDefense())
                attacker = m;
            return;
        }

        // Elegir defensor → batalla
        if (m != attacker)
        {
            ResolveBattle(attacker, m);
            attacker = null;
        }
    }

    // Calculo de batalla
    void ResolveBattle(MonsterCard a, MonsterCard d)
    {
        int atk = a.AttackPoints();
        int defVal = d.isDefense() ? d.DefensePoints() : d.AttackPoints();

        if (atk > defVal)
            d.SetOnField(false);
        else if (atk < defVal)
            a.SetOnField(false);
        else
        {
            a.SetOnField(false);
            d.SetOnField(false);
        }
    }
}

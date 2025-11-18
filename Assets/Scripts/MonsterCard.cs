using UnityEngine;

public class MonsterCard : MonoBehaviour
{

    [SerializeField] string monsterName;
    [SerializeField] int attackPoints;
    [SerializeField] int defensePoints;

    bool isOnField = false;
    bool isInDefense = false;

    void Start()
    {
        isOnField = false;
        isInDefense = false;
    }

    public void SetOnField(bool active)
    {
        isOnField = active;
        gameObject.SetActive(active);

        if (!active)
        {
            isInDefense = false;
            transform.rotation = Quaternion.identity;
        }
    }

    public void ToggleDefense()
    {
        if (isInDefense) isInDefense = false;
        else isInDefense = false;

    }

    public int AttackPoints() { return attackPoints; }
    public int DefensePoints() { return defensePoints; }

    public bool isDefense() { return isInDefense; }






}

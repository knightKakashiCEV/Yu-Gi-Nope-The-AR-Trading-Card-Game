using UnityEngine;

public class MonsterCard : MonoBehaviour
{
    [SerializeField] string monsterName;
    [SerializeField] int attackPoints;
    [SerializeField] int defensePoints;

    bool isOnField;
    bool isInDefense;

    bool hasChangedPositionThisTurn;
    bool hasAttackedThisTurn;

    void Start()
    {
        // Si el monstruo está activo en la escena, asumimos que está en el campo.
        isOnField = gameObject.activeSelf;
        isInDefense = false;
        hasChangedPositionThisTurn = false;
        hasAttackedThisTurn = false;
    }

    public void SetOnField(bool active)
    {
        isOnField = active;
        gameObject.SetActive(active);

        if (!active)
        {
            isInDefense = false;
            transform.rotation = Quaternion.identity;
            hasChangedPositionThisTurn = false;
            hasAttackedThisTurn = false;
        }
    }

    // --- Gestión de turno ---

    public void ResetTurnFlags()
    {
        hasChangedPositionThisTurn = false;
        hasAttackedThisTurn = false;
    }

    public bool CanChangePosition()
    {
        return isOnField && !hasChangedPositionThisTurn;
    }

    public void ToggleDefense()
    {
        if (!CanChangePosition())
            return;

        isInDefense = !isInDefense;
        hasChangedPositionThisTurn = true;

        // Rotación simple para indicar defensa (90º)
        if (isInDefense)
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        else
            transform.rotation = Quaternion.identity;
    }

    public bool CanBeAttacker()
    {
        // Solo puede atacar si está en ataque, en campo y no ha atacado aún este turno
        return isOnField && !isInDefense && !hasAttackedThisTurn;
    }

    public void MarkAsAttacked()
    {
        hasAttackedThisTurn = true;
    }

    // --- Getters sencillos ---

    public int AttackPoints() => attackPoints;
    public int DefensePoints() => defensePoints;

    public bool IsDefense() => isInDefense;
    public bool IsOnField() => isOnField;
}

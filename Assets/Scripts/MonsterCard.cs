using UnityEngine;

public class MonsterCard : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private string monsterName;
    [SerializeField] private int attackPoints;
    [SerializeField] private int defensePoints;

    bool isOnField;
    bool isInDefense;
    bool hasChangedPositionThisTurn;
    bool hasAttackedThisTurn;

    Animator animator;

    void Start()
    {
        isOnField = gameObject.activeSelf;
        isInDefense = false;
        hasChangedPositionThisTurn = false;
        hasAttackedThisTurn = false;

        animator = GetComponent<Animator>();
        PlayIdle();
    }

    // Animation Controllers
    public void PlayIdle()
    {
        if (animator == null) return;
        animator.SetBool("Idle", true);
        animator.SetBool("Attack", false);
        animator.SetBool("Defense", false);
        animator.SetBool("Dead", false);
    }

    public void PlayAttackAnim()
    {
        if (animator == null) return;
        animator.SetBool("Attack", true);
        animator.SetBool("Idle", false);
    }

    public void PlayDieAnim()
    {
        if (animator == null) return;
        animator.SetBool("Dead", true);
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Defense", false);
    }


    // Set the monster on the playing "field"
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

    // Return the respective flags of changed position and if it has attacked.
    public void ResetTurnFlags()
    {
        hasChangedPositionThisTurn = false;
        hasAttackedThisTurn = false;
    }

    // Bools for checking if it can do various things such as change position, be the attacker or if it has attacked

    public bool CanChangePosition() => isOnField && !hasChangedPositionThisTurn;
    public bool CanReturnToAttack() => isOnField && isInDefense && !hasChangedPositionThisTurn;
    public bool CanBeAttacker() => isOnField && !isInDefense && !hasAttackedThisTurn;

    public void MarkAsAttacked() => hasAttackedThisTurn = true;


    // Toggle the monster into defense as well as rotating it. (Here the rotation can be deleted once there's a proper animation)
    public void ToggleDefense()
    {
        if (!CanChangePosition()) return;

        isInDefense = !isInDefense;
        hasChangedPositionThisTurn = true;

        if (isInDefense)
        {
            if (animator != null)
            {
                animator.SetBool("Defend", true);
                animator.SetBool("Idle", false);
            }
        }
        else
        {
            transform.rotation = Quaternion.identity;
            PlayIdle();
        }
    }


    // Change the monster back to Attack Position if it can.
    public void ReturnToAttack()
    {
        if (!CanReturnToAttack()) return;

        isInDefense = false;
        hasChangedPositionThisTurn = true;
        transform.rotation = Quaternion.identity;
        PlayIdle();
    }

    public int AttackPoints() => attackPoints;
    public int DefensePoints() => defensePoints;
    public bool IsDefense() => isInDefense;
    public bool IsOnField() => isOnField;
}

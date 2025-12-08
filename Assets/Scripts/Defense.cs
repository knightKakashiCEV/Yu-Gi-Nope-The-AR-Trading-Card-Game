using UnityEngine;

public class Defense : MonoBehaviour
{
    [SerializeField] private GameObject defenseButton;  
    [SerializeField] private GameObject attackPosButton; 

    private MonsterCard selected;

    void Awake()
    {
        gameObject.SetActive(false); 
    }

    // Open/Close the Position panel
    public void Open(MonsterCard m)
    {
        if (gameObject.activeSelf && selected == m)
        {
            Hide();
            return;
        }

        selected = m;

        bool isDef = m.IsDefense();
        bool canToDef = !isDef && m.CanChangePosition();   
        bool canToAttack = isDef && m.CanReturnToAttack();  

        Debug.Log($"[DefensePanel] {m.name} | isDef={isDef} | canToDef={canToDef} | canToAttack={canToAttack}");

        // If nothing can be done, close and bail
        if (!canToDef && !canToAttack)
        {
            Hide();
            return;
        }

        // Set panel active and show only the correct button
        gameObject.SetActive(true);

        if (defenseButton != null)
            defenseButton.SetActive(canToDef);

        if (attackPosButton != null)
            attackPosButton.SetActive(canToAttack);
    }

    // rotate and set the Defense flags
    public void OnDefenseButton()
    {
        if (selected != null)
            selected.ToggleDefense();  

        Hide();
    }

    // rotate and set the Attack Pos. flags
    public void OnAttackPosButton()
    {
        if (selected != null)
            selected.ReturnToAttack(); 

        Hide();
    }

    // Hide the panel
    public void Hide()
    {
        selected = null;

        if (defenseButton != null)
            defenseButton.SetActive(false);
        if (attackPosButton != null)
            attackPosButton.SetActive(false);

        gameObject.SetActive(false);
    }
}

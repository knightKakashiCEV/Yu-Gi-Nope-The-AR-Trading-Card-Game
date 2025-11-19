using UnityEngine;

public class Defense : MonoBehaviour
{
    MonsterCard selected;

    public void SetSelected(MonsterCard m)
    {
        selected = m;
        gameObject.SetActive(true);   // Mostrar panel del botón Defensa
    }

    public void PressDefense()
    {
        if (selected != null)
            selected.ToggleDefense();

        gameObject.SetActive(false);
        selected = null;
    }
}

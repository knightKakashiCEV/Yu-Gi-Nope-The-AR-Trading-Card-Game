using UnityEngine;

public class Defense : MonoBehaviour
{
    MonsterCard selected;

    public void SetSelected(MonsterCard m)
    {
        selected = m;
        gameObject.SetActive(true);
    }

    public void PressDefense()
    {
        if (selected != null)
            selected.ToggleDefense();

        gameObject.SetActive(false);
        selected = null;
    }
}

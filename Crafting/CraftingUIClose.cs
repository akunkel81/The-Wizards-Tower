using UnityEngine;

public class CraftingUIClose : MonoBehaviour
{
    public GameObject craftingUI;

    public void Close()
    {
        if (craftingUI != null)
            craftingUI.SetActive(false);

        Time.timeScale = 1f;
    }
}
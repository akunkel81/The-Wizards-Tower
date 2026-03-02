using UnityEngine;

public class CraftingToggle : MonoBehaviour
{
    public GameObject craftingUI;   // Assign CraftingPanel here
    public bool pauseGame = true;

    private void Start()
    {
        if (craftingUI != null)
            craftingUI.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (craftingUI == null)
        {
            Debug.LogError("CauldronOpenCrafting: craftingUI not assigned.");
            return;
        }

        bool willOpen = !craftingUI.activeSelf;
        craftingUI.SetActive(willOpen);

        if (pauseGame)
            Time.timeScale = willOpen ? 0f : 1f;
    }

}
using UnityEngine;

public class RepairManager : MonoBehaviour
{
    public CraftingManager craftingManager;
    public SetPlayerCoins playerCoins;

    public GameObject repairUI;
    public GameObject repairMenu;

    public int halfRepairCost = 50;
    public int fullRepairCost = 100;


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (repairMenu == null)
            {
                Debug.LogError("RepairMenu not assigned.");
                return;
            }

            repairMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public bool TryHalfRepair()
    {
        if (craftingManager == null || playerCoins == null)
        {
            Debug.LogError("RepairManager: missing references.");
            return false;
        }

        if (craftingManager.currentDurability >= craftingManager.maxDurability)
        {
            Debug.Log("Cauldron already fully repaired.");
            return false;
        }

        bool spent = playerCoins.SpendCoins(halfRepairCost);
        if (!spent)
        {
            Debug.Log("Not enough coins for half repair.");
            return false;
        }

        craftingManager.RepairHalf();
        return true;
    }

    public bool TryFullRepair()
    {
        if (craftingManager == null || playerCoins == null)
        {
            Debug.LogError("RepairManager: missing references.");
            return false;
        }

        if (craftingManager.currentDurability >= craftingManager.maxDurability)
        {
            Debug.Log("Cauldron already fully repaired.");
            return false;
        }

        bool spent = playerCoins.SpendCoins(fullRepairCost);
        if (!spent)
        {
            Debug.Log("Not enough coins for full repair.");
            return false;
        }

        craftingManager.RepairFull();
        return true;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradeUI;
    public GameObject upgradeMenu;

    private HashSet<Upgrade> purchasedUpgrades = new HashSet<Upgrade>();

    private void OnMouseDown()
    {
        if (upgradeMenu == null)
        {
            Debug.LogError("UpgradeManager: upgradeUI not assigned.");
            return;
        }

        upgradeMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public bool IsPurchased(Upgrade upgrade)
    {
        if (upgrade == null) return false;
        return purchasedUpgrades.Contains(upgrade);
    }

    public bool MarkPurchased(Upgrade upgrade)
    {
        if (upgrade == null) return false;
        return purchasedUpgrades.Add(upgrade);
    }
}
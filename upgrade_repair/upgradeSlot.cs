using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class upgradeSlot : MonoBehaviour, IPointerClickHandler
{
    [Header("UI")]
    public TextMeshProUGUI nameText;

    [Header("Runtime")]
    public Upgrade upgrade;
    private UpgradeUIController upgradeUIController;

     public void Bind(Upgrade u, UpgradeUIController ui)
    {
        upgrade = u;
        upgradeUIController = ui;

        if (nameText != null) nameText.text = u != null ? u.upgradeName : "(None)";

    }


   public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (upgradeUIController == null || upgrade == null) return;

        upgradeUIController.SelectUpgrade(upgrade);
    }
}
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CraftingSlot : MonoBehaviour, IPointerClickHandler
{
    [Header("UI")]
    public TMP_Text nameText;
    public Image iconImage;

    [Header("Runtime")]
    public PotionSO potion;
    private CraftingUIController craftingUIController;

     public void Bind(PotionSO p, CraftingUIController ui)
    {
        potion = p;
        craftingUIController = ui;

        if (nameText != null) nameText.text = p != null ? p.displayName : "(None)";

        if (iconImage != null)
        {
            iconImage.sprite = (p != null) ? p.potionSprite : null;
            iconImage.enabled = (iconImage.sprite != null);
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (craftingUIController == null || potion == null) return;

        craftingUIController.SelectPotion(potion);
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingUIController : MonoBehaviour
{
    [Header("Data")]
    public ItemDatabaseSO itemDatabase;

    [Header("Left List")]
    public CraftingSlot[] slots;

    [Header("Right Detail UI")]
    public TMP_Text potionNameText;
    public Image potionImage;
    public TMP_Text potionDescriptionText;

    [Header("Recipe Lines")]
    public RecipeLineUI[] recipeLines;

    [Header("Craft Button")]
    public Button craftButton;

    private PotionSO _selectedPotion;
    private InventoryManager _inventory;

    private void Awake()
    {
        _inventory = FindFirstObjectByType<InventoryManager>();
    }

    private void OnEnable()
    {
        PopulateLeftList();
        ClearRightSide();
    }

    private void PopulateLeftList()
    {
        if (itemDatabase == null || itemDatabase.potions == null)
        {
            Debug.LogError("CraftingUI: itemDatabase missing or potions empty.");
            return;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            PotionSO p = (i < itemDatabase.potions.Count) ? itemDatabase.potions[i] : null;

            if (slots[i] != null)
            {
                slots[i].gameObject.SetActive(p != null);
                if (p != null) slots[i].Bind(p, this);
            }
        }
    }

    public void SelectPotion(PotionSO potion)
    {
        _selectedPotion = potion;

        if (potionNameText != null) potionNameText.text = potion.displayName;

        if (potionImage != null)
        {
            potionImage.sprite = potion.potionSprite;
            potionImage.enabled = (potionImage.sprite != null);
        }

        if (potionDescriptionText != null)
        {
            int sell = (GameManager.Instance != null)
                ? GameManager.Instance.GetEffectivePotionSellPrice(potion)
                : potion.sellPrice;

            potionDescriptionText.text =
                "Sell Price: " + sell + "\n" +
                "Rarity: " + potion.rarity + "\n\n" +
                potion.itemDescription;
        }

        UpdateRecipeUI(potion);
        UpdateCraftButtonInteractable(potion);
    }

    private void UpdateRecipeUI(PotionSO potion)
    {
        for (int i = 0; i < recipeLines.Length; i++)
        {
            recipeLines[i].SetEmpty();
        }

        if (potion.recipe == null) return;

        for (int i = 0; i < potion.recipe.Length && i < recipeLines.Length; i++)
        {
            var entry = potion.recipe[i];
            if (entry == null || entry.ingredient == null) continue;

            int have = (_inventory != null) ? _inventory.GetIngredientCount(entry.ingredient) : 0;
            int need = entry.quantity;

            recipeLines[i].Set(entry.ingredient.displayName, need, have);
        }
    }

    private void UpdateCraftButtonInteractable(PotionSO potion)
    {
        if (craftButton == null) return;

        bool canCraft = CanCraft(potion);
        craftButton.interactable = canCraft;
    }

    private bool CanCraft(PotionSO potion)
    {
        if (potion == null || potion.recipe == null || _inventory == null) return false;

        foreach (var entry in potion.recipe)
        {
            if (entry == null || entry.ingredient == null) return false;

            int have = _inventory.GetIngredientCount(entry.ingredient);
            if (have < entry.quantity) return false;
        }

        return true;
    }

    private void ClearRightSide()
    {
        _selectedPotion = null;

        if (potionNameText != null) potionNameText.text = "";
        if (potionDescriptionText != null) potionDescriptionText.text = "";

        if (potionImage != null)
        {
            potionImage.sprite = null;
            potionImage.enabled = false;
        }

        for (int i = 0; i < recipeLines.Length; i++)
        {
            recipeLines[i].SetEmpty();
        }

        if (craftButton != null) craftButton.interactable = false;
    }

    public void OnClickCraft()
    {
        if (_selectedPotion == null) return;
        if (!CanCraft(_selectedPotion)) return;

        // Next step: remove ingredients + add potion to inventory
        Debug.Log("Crafted: " + _selectedPotion.displayName);
    }
}
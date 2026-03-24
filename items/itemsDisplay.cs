using UnityEngine;
using TMPro;

public class ItemsDisplay : MonoBehaviour
{
    public TextMeshProUGUI itemsTextbox;

    [Header("Data")]
    public ItemDatabaseSO itemDatabase;

    [Header("Sample Names")]
    public string sampleIngredientName = "Sage";
    public string samplePotionName = "Healing Potion";

    private void Start()
    {
        UpdateItemsText();
    }

    public void UpdateItemsText()
    {
        if (itemsTextbox == null)
        {
            Debug.LogError("ItemsDisplay: itemsTextbox not assigned.");
            return;
        }

        if (GameManager.Instance == null)
        {
            itemsTextbox.text = "GameManager missing.";
            return;
        }

        if (itemDatabase == null)
        {
            itemsTextbox.text = "ItemDatabase not assigned.";
            return;
        }

        if (itemDatabase.ingredients == null || itemDatabase.ingredients.Count == 0)
        {
            itemsTextbox.text = "No ingredients found in database.";
            return;
        }

        if (itemDatabase.potions == null || itemDatabase.potions.Count == 0)
        {
            itemsTextbox.text = "No potions found in database.";
            return;
        }

        var gm = GameManager.Instance;

        IngredientSO sampleIng = FindIngredient(sampleIngredientName);
        if (sampleIng == null) sampleIng = itemDatabase.ingredients[0];

        PotionSO samplePotion = FindPotion(samplePotionName);
        if (samplePotion == null) samplePotion = itemDatabase.potions[0];

        float availMult = gm.GetAvailabilityMultiplierForType(sampleIng.itemType);
        float effectiveWeight = gm.GetEffectiveIngredientWeight(sampleIng);

        int basePotionPrice = samplePotion.sellPrice;
        int effectivePotionPrice = gm.GetEffectivePotionSellPrice(samplePotion);

        itemsTextbox.text = "";

        itemsTextbox.text +=
            "Ingredient Example:\n" +
            sampleIng.displayName + " | " + sampleIng.itemType +
            " | base price " + sampleIng.itemPrice +
            " | max " + sampleIng.maxPerCharacter +
            " | base rarity " + sampleIng.rarity + "\n" +
            "Availability multiplier (" + sampleIng.itemType + ") = " + availMult + "\n" +
            "Effective rarity weight = " + effectiveWeight + "\n\n";

        itemsTextbox.text +=
            "Potion Example:\n" +
            samplePotion.displayName + "\n" +
            "Base sell price = " + basePotionPrice + "\n" +
            "Modified sell price = " + effectivePotionPrice + "\n" +
            "Recipe:\n";

        if (samplePotion.recipe != null)
        {
            foreach (var r in samplePotion.recipe)
            {
                if (r == null || r.ingredient == null) continue;
                itemsTextbox.text += "- " + r.ingredient.displayName + " x" + r.quantity + "\n";
            }
        }
    }

    private IngredientSO FindIngredient(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;

        foreach (var ing in itemDatabase.ingredients)
        {
            if (ing != null && ing.displayName == name) return ing;
        }
        return null;
    }

    private PotionSO FindPotion(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;

        foreach (var p in itemDatabase.potions)
        {
            if (p != null && p.displayName == name) return p;
        }
        return null;
    }
}
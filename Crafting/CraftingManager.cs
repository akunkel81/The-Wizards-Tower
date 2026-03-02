using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public PotionSO potionToCraft; // assign Healing Potion for testing

    public bool TryCraft(PotionSO potion)
    {
        if (potion == null) return false;

        InventoryManager inv = FindFirstObjectByType<InventoryManager>();
        if (inv == null)
        {
            Debug.LogError("CraftingSystem: InventoryManager missing.");
            return false;
        }

        if (potion.recipe == null || potion.recipe.Length == 0)
        {
            Debug.LogError("CraftingSystem: Potion has no recipe.");
            return false;
        }

        // 1) Check
        foreach (var entry in potion.recipe)
        {
            if (entry == null || entry.ingredient == null) continue;

            int owned = inv.GetIngredientCount(entry.ingredient);
            if (owned < entry.quantity)
            {
                Debug.Log("Cannot craft " + potion.displayName + ". Missing " + entry.ingredient.displayName);
                return false;
            }
        }

        // 2) Remove
        foreach (var entry in potion.recipe)
        {
            if (entry == null || entry.ingredient == null) continue;

            bool removed = inv.TryRemoveIngredient(entry.ingredient, entry.quantity);
            if (!removed)
            {
                Debug.LogError("CraftingSystem: Remove failed unexpectedly.");
                return false;
            }
        }

        inv.AddPotion(potion, 1);

        Debug.Log("Crafted: " + potion.displayName);

        // Step 3 later: add potion to inventory
        return true;
    }

    private void OnMouseDown()
    {
        // Test crafting by clicking cauldron
        TryCraft(potionToCraft);
    }
}
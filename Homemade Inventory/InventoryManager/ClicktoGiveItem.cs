using UnityEngine;

public class ClickToGiveItem : MonoBehaviour
{
    public IngredientSO ingredientToGive1;
    public int amount1 = 1;

    public IngredientSO ingredientToGive2;
    public int amount2 = 1;

    private bool alreadyTaken = false;

    private void OnMouseDown()
    {
        if (alreadyTaken)
        {
            Debug.Log("You already took the items.");
            return;
        }

        InventoryManager inv = FindFirstObjectByType<InventoryManager>();
        if (inv == null)
        {
            Debug.LogError("ClickToGiveItem: InventoryManager not found.");
            return;
        }

        bool gaveAnything = false;

        if (ingredientToGive1 != null && amount1 > 0)
        {
            inv.AddIngredient(
                ingredientToGive1,
                amount1,
                ingredientToGive1.displayName,
                ingredientToGive1.itemType,
                ingredientToGive1.itemPrice,
                ingredientToGive1.maxPerCharacter,
                ingredientToGive1.rarity,
                ingredientToGive1.itemDescription
            );
            Debug.Log("Gave " + ingredientToGive1.displayName + " x" + amount1);
            gaveAnything = true;
        }

        if (ingredientToGive2 != null && amount2 > 0)
        {
            inv.AddIngredient(
                ingredientToGive2,
                amount2,
                ingredientToGive2.displayName,
                ingredientToGive2.itemType,
                ingredientToGive2.itemPrice,
                ingredientToGive2.maxPerCharacter,
                ingredientToGive2.rarity,
                ingredientToGive2.itemDescription
            );
            Debug.Log("Gave " + ingredientToGive2.displayName + " x" + amount2);
            gaveAnything = true;
        }

        if (!gaveAnything)
        {
            Debug.LogError("ClickToGiveItem: No ingredients assigned to give.");
            return;
        }

        alreadyTaken = true;
        Debug.Log("Items collected.");
    }
}
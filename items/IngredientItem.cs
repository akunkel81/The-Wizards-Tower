using UnityEngine;

public class IngredientItem : MonoBehaviour
{
    public IngredientSO ingredientData;

    private void Start()
    {
        if (ingredientData == null)
        {
            Debug.LogError("IngredientItem: ingredientData not assigned on " + gameObject.name);
            return;
        }

        Debug.Log(
            "Ingredient loaded: " +
            ingredientData.displayName +
            " | type=" + ingredientData.itemType +
            " | price=" + ingredientData.itemPrice +
            " | rarity=" + ingredientData.rarity
        );
    }
}
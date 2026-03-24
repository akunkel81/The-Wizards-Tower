using UnityEngine;

public class PotionItem : MonoBehaviour
{
    public PotionSO potionData;

    private void Start()
    {
        if (potionData == null)
        {
            Debug.LogError("PotionItem: potionData is not assigned on " + gameObject.name);
            return;
        }

        Debug.Log(
            "PotionItem loaded: " + potionData.displayName +
            " | rarity=" + potionData.rarity +
            " | sellPrice=" + potionData.sellPrice +
            " | recipeCount=" + (potionData.recipe != null ? potionData.recipe.Length : 0)
        );

        if (potionData.recipe != null)
        {
            foreach (var r in potionData.recipe)
            {
                if (r != null && r.ingredient != null)
                {
                    Debug.Log("  Recipe: " + r.ingredient.displayName + " x" + r.quantity);
                }
            }
        }
    }
}
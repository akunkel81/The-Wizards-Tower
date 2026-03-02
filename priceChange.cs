using UnityEngine;

public class priceChange : MonoBehaviour
{
    public ItemsLoader loader = GameManager.Instance.itemsLoader;
    public float priceChangeAmount = 1.2f; // Example: 20% price change increase

    public void ApplyPriceChange()
    {
        if (loader == null || loader.itemsRoot == null || loader.itemsRoot.items == null || loader.itemsRoot.items.potions == null)
        {
            Debug.LogError("priceChange: Items data not loaded correctly.");
            return;
        }

        foreach (var potion in loader.itemsRoot.items.potions)
        {
            potion.sellPrice = Mathf.RoundToInt(potion.sellPrice * priceChangeAmount);
        }

        Debug.Log("Applied price change to all potions. New prices:");
        foreach (var potion in loader.itemsRoot.items.potions)
        {
            Debug.Log(potion.name + ": " + potion.sellPrice);
        }
    }

}

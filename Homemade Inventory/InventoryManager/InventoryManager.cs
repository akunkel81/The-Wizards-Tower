using UnityEngine;
using XEntity.InventoryItemSystem;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
   public GameObject InventoryMenu;
   private bool menuActivated;
   public ItemSlot[] itemSlots;
    public Dictionary<IngredientSO, int> ingredientCounts = new Dictionary<IngredientSO, int>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && menuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if (Input.GetKeyDown(KeyCode.I) && !menuActivated)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }

        
    }
    public int AddPotion(PotionSO potion, int quantity)
    {
        if (potion == null || quantity <= 0) return 0;

        int remaining = quantity;

        // 1) Fill existing stacks
        for (int i = 0; i < itemSlots.Length; i++)
        {
            ItemSlot slot = itemSlots[i];
            if (slot == null) continue;

            if (slot.isFull && slot.isPotion && slot.potionData == potion)
            {
                remaining = slot.AddPotion(potion, remaining);
                if (remaining <= 0) return 0;
            }
        }

        // 2) Empty slots
        for (int i = 0; i < itemSlots.Length; i++)
        {
            ItemSlot slot = itemSlots[i];
            if (slot == null) continue;

            if (!slot.isFull || slot.quantity == 0)
            {
                remaining = slot.AddPotion(potion, remaining);
                if (remaining <= 0) return 0;
            }
        }

        return remaining;
    }
     public int AddIngredient(
        IngredientSO ingredient,
        int quantity,
        string displayName,
        string itemType,
        int itemPrice,
        int maxPerCharacter,
        float rarity,
        string itemDescription)
    {
        if (ingredient == null || quantity <= 0) return 0;

        Debug.Log(
            "itemName " + displayName +
            " quantity " + quantity +
            " itemType " + itemType +
            " itemPrice " + itemPrice +
            " maxPerCharacter " + maxPerCharacter +
            " rarity " + rarity);

        int remaining = quantity;

        // 1) Fill existing stacks first (same ingredient)
        for (int i = 0; i < itemSlots.Length; i++)
        {
            ItemSlot slot = itemSlots[i];
            if (slot == null) continue;

            if (slot.isFull && slot.ingredientData == ingredient)
            {
                remaining = slot.AddIngredient(
                    ingredient,
                    remaining,
                    displayName,
                    itemType,
                    itemPrice,
                    maxPerCharacter,
                    rarity,
                    itemDescription
                );

                if (remaining <= 0)
                {
                    AddToTotals(ingredient, quantity);
                    return 0;
                }
            }
        }

        // 2) Use empty slots for leftovers
        for (int i = 0; i < itemSlots.Length; i++)
        {
            ItemSlot slot = itemSlots[i];
            if (slot == null) continue;

            if (!slot.isFull || slot.quantity == 0)
            {
                remaining = slot.AddIngredient(
                    ingredient,
                    remaining,
                    displayName,
                    itemType,
                    itemPrice,
                    maxPerCharacter,
                    rarity,
                    itemDescription
                );

                if (remaining <= 0)
                {
                    AddToTotals(ingredient, quantity);
                    return 0;
                }
            }
        }

        // 3) Could not fit everything
        int successfullyAdded = quantity - remaining;
        if (successfullyAdded > 0)
        {
            AddToTotals(ingredient, successfullyAdded);
        }

        return remaining;
    }

    private void AddToTotals(IngredientSO ingredient, int amountAdded)
    {
        if (amountAdded <= 0) return;

        if (!ingredientCounts.ContainsKey(ingredient))
        {
            ingredientCounts[ingredient] = 0;
        }

        ingredientCounts[ingredient] += amountAdded;
    }

      public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].selectedShader.SetActive(false);
            itemSlots[i].thisItemSelected = false;
    
        }
    }

   public int GetIngredientCount(IngredientSO ingredient)
    {
        if (ingredient == null) return 0;

        int total = 0;
        for (int i = 0; i < itemSlots.Length; i++)
        {
            ItemSlot slot = itemSlots[i];
            if (slot == null) continue;

            if (slot.isFull && slot.ingredientData == ingredient)
            {
                total += slot.quantity;
            }
        }
        return total;
    }

    public bool TryRemoveIngredient(IngredientSO ingredient, int amount)
{
    if (ingredient == null || amount <= 0) return false;

    int have = GetIngredientCount(ingredient);
    if (have < amount) return false;

    int remainingToRemove = amount;

    for (int i = 0; i < itemSlots.Length; i++)
    {
        ItemSlot slot = itemSlots[i];
        if (slot == null) continue;

        if (!slot.isFull || slot.ingredientData != ingredient) continue;

        if (remainingToRemove <= 0) break;

        int removeHere = Mathf.Min(slot.quantity, remainingToRemove);
        slot.quantity -= removeHere;
        remainingToRemove -= removeHere;

        // Update slot UI and empty state
        if (slot.quantity <= 0)
        {
            slot.quantity = 0;
            slot.isFull = false;
            slot.ingredientData = null;
            slot.displayName = "";
            slot.itemSprite = null;
            slot.itemDescription = "";

            slot.ClearUI(); // You will add this method if you do not have it.
        }
        else
        {
            slot.UpdateUI(); // You will add this method if you do not have it.
        }
    }
    if (ingredientCounts.ContainsKey(ingredient))
    {
        ingredientCounts[ingredient] -= amount;
        if (ingredientCounts[ingredient] < 0) ingredientCounts[ingredient] = 0;
    }

    return true;
}

}


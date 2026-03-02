using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
   //======ITEM DATA======//
   public string displayName;
   public int quantity;
   public Sprite itemSprite;
   public bool isFull;
   public string itemDescription;
   public IngredientSO ingredientData;
   
   //====ITEM SLOT===//
   [SerializeField]
   private TMP_Text quantityText;

   [SerializeField]
    private Image itemImage;

    //====ITEM DESCRIPTION SLOT===//
    
    
[SerializeField]
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    
   
    public GameObject selectedShader;
    public bool thisItemSelected;
    private InventoryManager inventoryManager;

//=====POTION DATA=====//
    public PotionSO potionData;
    public bool isPotion;  

private void Start()
    {
        inventoryManager = FindFirstObjectByType<InventoryManager>();
    }
public int AddPotion(PotionSO potion, int amount)
{
    if (potion == null || amount <= 0) return 0;

    // If empty, initialize as potion slot
    if (!isFull || quantity == 0)
    {
        isPotion = true;
        potionData = potion;

        ingredientData = null; // clear ingredient mode
        displayName = potion.displayName;
        itemSprite = potion.potionSprite; // rename if your field is different

        quantity = 0;
        isFull = true;

        itemImage.sprite = itemSprite;
        itemImage.enabled = (itemImage.sprite != null);
    }

    // If slot has different potion or is an ingredient slot, reject
    if (!isPotion || potionData != potion) return amount;

    int maxStack = 99; // or potion.maxStackSize if you add it
    int space = maxStack - quantity;
    int toAdd = Mathf.Clamp(amount, 0, space);

    quantity += toAdd;

    quantityText.text = quantity.ToString();
    quantityText.enabled = true;

    return amount - toAdd;
}
public int AddIngredient(IngredientSO ingredient, int amount, string displayName, string itemType, int itemPrice, int maxPerCharacter, float rarity, string itemDescription)
{
    if (ingredient == null || amount <= 0) return 0;

    // If empty, initialize the slot
    if (!isFull || quantity == 0)
    {
        ingredientData = ingredient;
        this.displayName = displayName;
        this.itemSprite = ingredient.itemSprite;
        quantity = 0;
        isFull = true;

        itemImage.sprite = this.itemSprite;
        itemImage.enabled = (itemImage.sprite != null);
    }

    // If slot has a different item, reject all
    if (ingredientData != ingredient) return amount;

    int maxStack = ingredient.isStackable ? ingredient.maxStackSize : 1;

    int space = maxStack - quantity;
    int toAdd = Mathf.Clamp(amount, 0, space);

    quantity += toAdd;

    quantityText.text = quantity.ToString();
    quantityText.enabled = true;

    int leftover = amount - toAdd;

    // If this is a non stackable, always leftover after 1
    return leftover;
}
    public void OnPointerClick(PointerEventData eventData)
    {
        ShowDescription();
    }

    public void OnRightClick()
    {
    
    }

    private void ShowDescription()
{
    if (isPotion && potionData != null)
    {
        if (itemDescriptionImage != null)
        {
            itemDescriptionImage.sprite = potionData.potionSprite;
            itemDescriptionImage.enabled = (itemDescriptionImage.sprite != null);
        }

        if (itemDescriptionNameText != null)
            itemDescriptionNameText.text = potionData.displayName;

        int currentSell = potionData.sellPrice;
        if (GameManager.Instance != null)
            currentSell = GameManager.Instance.GetEffectivePotionSellPrice(potionData);

        if (itemDescriptionText != null)
        {
            itemDescriptionText.text =
                "Potion\n" +
                "Base Sell Price: " + potionData.sellPrice + "\n" +
                "Current Sell Price: " + currentSell + "\n" +
                "Owned: " + quantity;
        }

        return;
    }

    // Ingredient path (your existing code)
    if (ingredientData == null) return;

    if (itemDescriptionImage != null)
    {
        itemDescriptionImage.sprite = ingredientData.itemSprite;
        itemDescriptionImage.enabled = (ingredientData.itemSprite != null);
    }

    if (itemDescriptionNameText != null)
        itemDescriptionNameText.text = ingredientData.displayName;

    int currentPrice = ingredientData.itemPrice;
    if (GameManager.Instance != null)
        currentPrice = GameManager.Instance.GetEffectiveIngredientPrice(ingredientData);

    if (itemDescriptionText != null)
    {
        itemDescriptionText.text =
            "Type: " + ingredientData.itemType + "\n" +
            "Base Price: " + ingredientData.itemPrice + "\n" +
            "Current Price: " + currentPrice + "\n" +
            "Max Per Character: " + ingredientData.maxPerCharacter + "\n" +
            "Rarity: " + ingredientData.rarity + "\n\n" +
            ingredientData.itemDescription;
    }
}

public void UpdateUI()
{
    if (quantityText != null)
    {
        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
    }
}

public void ClearUI()
{
    if (quantityText != null)
    {
        quantityText.text = "";
        quantityText.enabled = false;
    }

    if (itemImage != null)
    {
        itemImage.sprite = null;
        itemImage.enabled = false;
    }

    selectedShader.SetActive(false);
    thisItemSelected = false;
}

}

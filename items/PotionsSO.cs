using UnityEngine;

public class PotionSO : ScriptableObject
{
    public Sprite potionSprite;
    public string displayName;
    public float rarity;
    public int sellPrice;
    public PotionRecipeEntrySO[] recipe;

     [TextArea]
    public string itemDescription;

    public int maxStackSize = 99;
    public bool isStackable = true;
}

[System.Serializable]
public class PotionRecipeEntrySO
{
    public IngredientSO ingredient;
    public int quantity;
}
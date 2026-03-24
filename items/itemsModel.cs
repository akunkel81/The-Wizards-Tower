using System;

//
// Matches the JSON format:
//
// {
//   "items": {
//     "ingredients": [ ... ],
//     "potions": [ ... ]
//   }
// }
//

[Serializable]
public class ItemsRoot
{
    public Items items;
}

[Serializable]
public class Items
{
    public Ingredient[] ingredients;
    public Potion[] potions;
}

[Serializable]
public class Ingredient
{
    public string name;
    public string itemType;
    public int itemPrice;
    public int maxPerCharacter;
    public float rarity;
}

[Serializable]
public class Potion
{
    public string name;
    public float rarity;
    public int sellPrice;
    public RecipeEntry[] recipe;
}

[Serializable]
public class RecipeEntry
{
    public string ingredientName;
    public int quantity;

    // Filled in after loading by building a lookup dictionary.
    [NonSerialized] public Ingredient resolvedIngredient;
}
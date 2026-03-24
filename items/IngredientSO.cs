using UnityEngine;

[CreateAssetMenu(menuName = "WizardTower/Ingredient")]
public class IngredientSO : ScriptableObject
{
    public Sprite itemSprite;
    public string displayName;
    public string itemType;
    public int itemPrice;
    public int maxPerCharacter;
    public float rarity;
    public GameObject prefab;

    [TextArea]
    public string itemDescription;

    public int maxStackSize = 99;
    public bool isStackable = true;
}

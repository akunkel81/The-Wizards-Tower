using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemsLoader : MonoBehaviour
{
    public static ItemsLoader Instance { get; private set; }
    public ItemsRoot itemsRoot;

    // Optional: fast lookup so you can do GetIngredient("Sage")
    public Dictionary<string, Ingredient> ingredientByName = new Dictionary<string, Ingredient>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadItemsData()
    {
        string fileName = "items.json";
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (!File.Exists(filePath))
        {
            Debug.LogError("Items data file not found at " + filePath);
            return;
        }

        string jsonData = File.ReadAllText(filePath);
        itemsRoot = JsonUtility.FromJson<ItemsRoot>(jsonData);

        if (itemsRoot == null || itemsRoot.items == null)
        {
            Debug.LogError("JSON parsed but items object is null. Check JSON format.");
            return;
        }

        if (itemsRoot.items.ingredients == null)
        {
            Debug.LogError("JSON parsed but ingredients array is null. Check JSON field names.");
            return;
        }

        if (itemsRoot.items.potions == null)
        {
            Debug.LogError("JSON parsed but potions array is null. Check JSON field names.");
            return;
        }

        BuildIngredientLookup();
        ResolvePotionRecipes();

        Debug.Log("Items data loaded successfully.");
        Debug.Log($"Loaded {itemsRoot.items.ingredients.Length} ingredients.");
        Debug.Log($"Loaded {itemsRoot.items.potions.Length} potions.");
    }

    private void BuildIngredientLookup()
    {
        ingredientByName.Clear();

        foreach (var ing in itemsRoot.items.ingredients)
        {
            if (ing == null || string.IsNullOrWhiteSpace(ing.name)) continue;

            string key = ing.name.Trim();

            if (!ingredientByName.ContainsKey(key))
            {
                ingredientByName.Add(key, ing);
            }
            else
            {
                Debug.LogWarning("Duplicate ingredient name in JSON: " + key);
            }
        }
    }

    private void ResolvePotionRecipes()
    {
        foreach (var potion in itemsRoot.items.potions)
        {
            if (potion == null || potion.recipe == null) continue;

            foreach (var entry in potion.recipe)
            {
                if (entry == null || string.IsNullOrWhiteSpace(entry.ingredientName)) continue;

                string key = entry.ingredientName.Trim();

                if (ingredientByName.TryGetValue(key, out var ing))
                {
                    entry.resolvedIngredient = ing;
                }
                else
                {
                    Debug.LogError($"Potion '{potion.name}' references missing ingredient '{key}'.");
                }
            }
        }
    }

    // Convenience method for other scripts:
    public Ingredient GetIngredient(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;

        ingredientByName.TryGetValue(name.Trim(), out var ing);
        return ing;
    }
}
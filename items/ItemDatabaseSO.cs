using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WizardTower/Item Database")]
public class ItemDatabaseSO : ScriptableObject
{
    public List<IngredientSO> ingredients = new List<IngredientSO>();
    public List<PotionSO> potions = new List<PotionSO>();
}
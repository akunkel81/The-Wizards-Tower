using UnityEngine;

public enum UpgradeType
{
    Fortify,
    FlavorBoost,
    Efficient,
    Potency
}

[CreateAssetMenu(fileName = "Upgrade", menuName = "Scriptable Objects/Upgrade")]
public class Upgrade : ScriptableObject
{
    public string upgradeName;

    [TextArea]
    public string upgradeDescription;

    public int upgradeCost;

    public UpgradeType upgradeType;

    // Optional effect values depending on upgrade type
    public int durabilityBonus;
    public float sellPriceMultiplier = 1f;
    public int ingredientReduction;
}
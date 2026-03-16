using UnityEngine;

public class PriceChange : MonoBehaviour
{
    public float priceIncreaseAmount = 1.2f;
    public float priceDecreaseAmount = 0.8f;

    public void ApplyIncrease()
    {
        ApplyMultiplier(priceIncreaseAmount);
    }

    public void ApplyDecrease()
    {
        ApplyMultiplier(priceDecreaseAmount);
    }

    private void ApplyMultiplier(float multiplier)
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("PriceChange: GameManager missing.");
            return;
        }

        // Player affects potion sell prices only
        GameManager.Instance.potionSellMultiplier *= multiplier;

        FindFirstObjectByType<PriceStatusDisplay>()?.UpdateDisplay();
    }
}
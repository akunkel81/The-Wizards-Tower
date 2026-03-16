using UnityEngine;

public class NPCSpendCoins : MonoBehaviour
{
    public int cost = 10;

    private void OnMouseDown()
    {
        SetPlayerCoins coins = FindFirstObjectByType<SetPlayerCoins>();

        if (coins == null)
        {
            Debug.LogError("Coin system not found.");
            return;
        }

        bool success = coins.SpendCoins(cost);

        if (success)
        {
            Debug.Log("Player spent " + cost + " coins.");
        }
        else
        {
            Debug.Log("Not enough coins.");
        }
    }
}
using UnityEngine;
using TMPro;

public class SetPlayerCoins : MonoBehaviour
{
    public static SetPlayerCoins Instance { get; private set; }
    public TextMeshProUGUI coinAmount;

    [Header("Testing")]
    public bool startFreshEachPlay = true;
    public int startingCoins = 100;

    private int currentCoins;
    private bool _initialized;

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

    private void Start()
    {
        if (startFreshEachPlay)
        {
            StartNewGame();
        }
        else
        {
            LoadCoins();
        }

        UpdateCoinText();
    }

    public void StartNewGame()
    {
        currentCoins = startingCoins;
        SaveCoins();
        Debug.Log("Started fresh with coins = " + currentCoins);
    }

    public void LoadCoins()
    {
        currentCoins = PlayerPrefs.GetInt("PlayerCoins", startingCoins);
        Debug.Log("Loaded PlayerCoins = " + currentCoins);
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt("PlayerCoins", currentCoins);
        PlayerPrefs.Save();
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;
        UpdateCoinText();
        SaveCoins();
    }

    public bool SpendCoins(int amount)
    {
        if (currentCoins < amount)
        {
            Debug.Log("Not enough coins.");
            return false;
        }

        currentCoins -= amount;
        UpdateCoinText();
        SaveCoins();
        return true;
    }

    private void UpdateCoinText()
    {
        if (coinAmount != null)
        {
            coinAmount.text = currentCoins.ToString();
        }
    }

    public int GetCurrentCoins()
    {
        return currentCoins;
    }

    public void ClearSavedCoins()
    {
        PlayerPrefs.DeleteKey("PlayerCoins");
        PlayerPrefs.Save();
        Debug.Log("Deleted saved PlayerCoins.");
    }
}
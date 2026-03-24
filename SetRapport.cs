//using UnityEngine;
//using TMPro;

//public class SetRapport : MonoBehaviour
//{
//    public static SetRapport Instance { get; private set; }
//    //public TextMeshProUGUI coinAmount;

//    [Header("Testing")]
//    //public bool startFreshEachPlay = true;
//    //public int startingCoins = 100;
//    public float startingRapport = 0f;

//    private float currentRapportFarmer;
//    private float currentRapportBaker;
//    private float currentRapportSalesman;
//    private float currentRapportSadFather;
//    private float currentRapportMiner;
//    private float currentRapportMoonGirl;
//    private float currentRapportGoldenWizard;
//    private bool _initialized;

//    private void Awake()
//    {
//        if (Instance != null && Instance != this)
//        {
//            Destroy(gameObject);
//            return;
//        }

//        Instance = this;
//        DontDestroyOnLoad(gameObject);
//    }

//    private void Start()
//    {
//        /*
//        if (startFreshEachPlay)
//        {
//            StartNewGame();
//        }
//        else
//        {
//            LoadRapport();
//        }

//        UpdateCoinText();
//        */
//    }

//    public void StartNewGame()
//    {
//        currentRapportFarmer = startingRapport;
//        currentRapportBaker = startingRapport;
//        currentRapportSalesman = startingRapport;
//        currentRapportSadFather = startingRapport;
//        currentRapportMiner = startingRapport;
//        currentRapportMoonGirl = startingRapport;
//        currentRapportGoldenWizard = startingRapport;

//        SaveRapport();
//        Debug.Log("All NPCS initialized with rapport value " + startingRapport);
//    }

//    public void LoadRapport()
//    {
//        // currentCoins = PlayerPrefs.GetInt("PlayerCoins", startingCoins);
//        // Debug.Log("Loaded PlayerCoins = " + currentCoins);
//    }

//    public void SaveRapport()
//    {
//        PlayerPrefs.SetFloat("RapportFarmer", currentRapportFarmer);
//        PlayerPrefs.SetFloat("RapportBaker", currentRapportBaker);
//        PlayerPrefs.SetFloat("RapportSalesman", currentRapportSalesman);
//        PlayerPrefs.SetFloat("RapportSadFather", currentRapportSadFather);
//        PlayerPrefs.SetFloat("RapportMiner", currentRapportMiner);
//        PlayerPrefs.SetFloat("RapportMoonGirl", currentRapportMoonGirl);
//        PlayerPrefs.SetFloat("RapportGoldenWizard", currentRapportGoldenWizard);
//        PlayerPrefs.Save();
//    }

//    public void ChangeRapport(SetRapport NPC, float amount)
//    {
//        //NPC += amount;
//        SaveRapport();
//    }

//    //private void UpdateCoinText()
//    //{
//    //    if (coinAmount != null)
//    //    {
//    //        coinAmount.text = currentCoins.ToString();
//    //    }
//    //}

//    public int GetCurrentRapport()
//    {
//        return 0; //placeholder
//    }

//    public void ClearSavedCoins()
//    {
//        PlayerPrefs.DeleteKey("PlayerCoins");
//        PlayerPrefs.Save();
//        Debug.Log("Deleted saved PlayerCoins.");
//    }
//}

// GOES UNUSED: Rapport saves by default

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class ActionManager : MonoBehaviour
{
    public bool IsInteractionLocked { get; private set; }
    public NPCDialogueTrigger npcDialogueTrigger;
    public int maxActionsPerTurn = 3;
    public TextMeshProUGUI actionsText;
    public Button skipActionButton;

    private int currentActions;
    private int currentNPCIndex;
    public NPCsSO[] npcQueue;
    public bool startFreshEachPlay = true;
    public static ActionManager Instance { get; private set; }

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
            StartNewDay();
        }
        else
        {
            LoadState();
            UpdateUI();
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject actionsObj = GameObject.Find("ActionsText");

        if (actionsObj != null)
        {
            actionsText = actionsObj.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogWarning("ActionsText object not found in scene.");
        }

        UpdateUI();
    }
    public void LoadState()
    {
        currentActions = PlayerPrefs.GetInt("CurrentActions", maxActionsPerTurn);
        currentNPCIndex = PlayerPrefs.GetInt("CurrentNPCIndex", 0);
    }

    public void SaveState()
    {
        PlayerPrefs.SetInt("CurrentActions", currentActions);
        PlayerPrefs.SetInt("CurrentNPCIndex", currentNPCIndex);
        PlayerPrefs.Save();
    }

    public bool UseAction(int amount = 1)
    {
        if (currentActions <= 0)
        {
            Debug.Log("No actions left. Triggering dialogue.");
            IsInteractionLocked = true;
            CloseAllMenus();
            
            TriggerDialogue();
            return false;
        }

        if (currentActions < amount)
        {
            Debug.Log("Not enough actions.");
            return false;
        }

        currentActions -= amount;
        SaveState();
        UpdateUI();

        Debug.Log("Action used. Remaining actions: " + currentActions);

        if (currentActions <= 0)
        {
            TriggerDialogue();
        }

        return true;
    }

    public void SkipAction()
    {
        UseAction(1);
    }

    public void OnClickSkipAction()
    {
        if (IsInteractionLocked)
        {
            Debug.Log("Skip blocked during NPC interaction.");
            return;
        }

        if (currentActions <= 0)
        {
            Debug.Log("No actions available.");
            return;
        }

        SkipAction();
    }

    public void ResetActionsForNextNPC()
    {
        currentActions = maxActionsPerTurn;
        IsInteractionLocked = false;
        SaveState();
        UpdateUI();
    }

    public int GetCurrentActions()
    {
        return currentActions;
    }

    public int GetCurrentNPCIndex()
    {
        return currentNPCIndex;
    }

    public void StartNewDay()
    {
        currentActions = maxActionsPerTurn;
        currentNPCIndex = 0;
        SaveState();
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (actionsText != null)
        {
            actionsText.text = "Actions: " + currentActions;
        }
        RefreshSkipButton();
    }

    public void TriggerDialogue()
    {
        Debug.Log("TriggerDialogue called");

        if (npcDialogueTrigger == null)
        {
            Debug.LogError("NPCDialogueTrigger is not assigned.");
            return;
        }

        npcDialogueTrigger.TriggerDialogue();
        RefreshSkipButton();
    }
    private void CloseAllMenus()
    {
        UpgradeUIController upgrade = FindFirstObjectByType<UpgradeUIController>();
        if (upgrade != null) upgrade.gameObject.SetActive(false);

        RepairUIController repair = FindFirstObjectByType<RepairUIController>();
        if (repair != null) repair.gameObject.SetActive(false);

        CraftingUIController crafting = FindFirstObjectByType<CraftingUIController>();
        if (crafting != null) crafting.gameObject.SetActive(false);
    }
    private bool hasEndedYear = false;

    public void FinishCurrentNPCDialogue()
    {
        if (hasEndedYear)
            return;

        currentNPCIndex++;

        if (npcQueue != null && currentNPCIndex >= npcQueue.Length)
        {
            hasEndedYear = true;

            if (GameManager.Instance != null)
                GameManager.Instance.EndYearOne();

            return;
        }

        ResetActionsForNextNPC();
        SaveState();
        RefreshSkipButton();
    }
    private void RefreshSkipButton()
    {
        if (skipActionButton != null)
        {
            skipActionButton.interactable = !IsInteractionLocked && currentActions > 0;
        }
    }
    public NPCsSO GetCurrentNPC()
    {
        if (npcQueue == null || npcQueue.Length == 0) return null;
        if (currentNPCIndex < 0 || currentNPCIndex >= npcQueue.Length) return null;

        return npcQueue[currentNPCIndex];
    }
}
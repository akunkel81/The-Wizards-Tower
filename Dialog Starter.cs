using System.Collections;
using UnityEngine;
using cherrydev;

public class NPCDialogueTrigger : MonoBehaviour
{
    private CraftingManager craftingManager;
    private RepairManager repairManager;
    private UpgradeManager upgradeManager;
    private InventoryManager inventoryManager;
    [SerializeField] private DialogBehaviour dialogBehaviour;
    [SerializeField] private ActionManager actionManager;
    [SerializeField] private NPCInventoryRuntime inventoryRuntime;
    [SerializeField] private NPCTradeFunctions npcTradeFunctions;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        if (actionManager == null)
            actionManager = FindFirstObjectByType<ActionManager>();

        if (inventoryRuntime == null)
            inventoryRuntime = FindFirstObjectByType<NPCInventoryRuntime>();

        if (npcTradeFunctions == null)
            npcTradeFunctions = FindFirstObjectByType<NPCTradeFunctions>();

        craftingManager = FindFirstObjectByType<CraftingManager>();
        repairManager = FindFirstObjectByType<RepairManager>();
        upgradeManager = FindFirstObjectByType<UpgradeManager>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();
    }

    public void TriggerDialogue()
    {
        Debug.Log("NPCDialogueTrigger.TriggerDialogue called");

        if (dialogBehaviour == null)
        {
            Debug.LogError("DialogBehaviour is not assigned.");
            return;
        }

        if (actionManager == null)
        {
            Debug.LogError("ActionManager is not assigned.");
            return;
        }

        NPCsSO currentNPC = actionManager.GetCurrentNPC();
        if (currentNPC == null)
        {
            Debug.LogError("No current NPC found in queue.");
            return;
        }

        if (inventoryRuntime != null)
        {
            inventoryRuntime.npcData = currentNPC;
            inventoryRuntime.BuildRuntimeInventory();
        }
        else
        {
            Debug.LogError("NPCInventoryRuntime is not assigned.");
            return;
        }

        if (npcTradeFunctions != null)
        {
            npcTradeFunctions.BindFunctions();
        }
        else
        {
            Debug.LogError("NPCTradeFunctions is not assigned.");
            return;
        }

        DialogNodeGraph selectedGraph = GetDialogueGraphForCurrentState(currentNPC);
        if (selectedGraph == null)
        {
            Debug.LogError("No dialogue graph found for NPC: " + currentNPC.characterName);
            return;
        }

        CloseAllMenus();
        StartCoroutine(StartDialogueNextFrame(selectedGraph));
    }

    private DialogNodeGraph GetDialogueGraphForCurrentState(NPCsSO npc)
    {
        if (npc == null) return null;

        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager missing. Falling back to bad year graph.");
            return npc.badYearGraph;
        }

        if (GameManager.Instance.IsYearOne())
            return npc.yearOneGraph;

        if (GameManager.Instance.currentYearEvent == null)
        {
            Debug.LogWarning("Current year event missing. Falling back to bad year graph.");
            return npc.badYearGraph;
        }

        if (GameManager.Instance.IsGoodYear())
            return npc.goodYearGraph;
        else
            return npc.badYearGraph;
    }

    private void CloseAllMenus()
    {
        if (craftingManager != null)
        {
            craftingManager.ForceCloseMenu();
            craftingManager.SetInputLocked(true);
        }

        if (repairManager != null)
        {
            repairManager.ForceCloseMenu();
            repairManager.SetInputLocked(true);
        }

        if (upgradeManager != null)
        {
            upgradeManager.ForceCloseMenu();
            upgradeManager.SetInputLocked(true);
        }

        if (inventoryManager != null && inventoryManager.InventoryMenu != null)
            inventoryManager.InventoryMenu.SetActive(false);

        Time.timeScale = 1f;
    }

    private IEnumerator StartDialogueNextFrame(DialogNodeGraph graph)
    {
        yield return null;
        dialogBehaviour.StartDialog(graph);
    }

    public void OnDialogueFinished()
    {
        Debug.Log("Dialogue finished");

        if (craftingManager != null)
            craftingManager.SetInputLocked(false);

        if (repairManager != null)
            repairManager.SetInputLocked(false);

        if (upgradeManager != null)
            upgradeManager.SetInputLocked(false);

        if (actionManager != null)
            actionManager.FinishCurrentNPCDialogue();
    }

}
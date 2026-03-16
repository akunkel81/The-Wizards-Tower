using UnityEngine;

public class RepairUIController : MonoBehaviour
{
    public RepairManager repairManager;
    public GameObject repairMenu;

    public void OnClickHalfRepair()
    {
        if (repairManager == null) return;

        bool success = repairManager.TryHalfRepair();
        if (success)
        {
            ActionManager actionManager = FindFirstObjectByType<ActionManager>();
            if (actionManager != null)
                actionManager.UseAction(1);
        }
    }

    public void OnClickFullRepair()
    {
        if (repairManager == null) return;

        bool success = repairManager.TryFullRepair();
        if (success)
        {
            ActionManager actionManager = FindFirstObjectByType<ActionManager>();
            if (actionManager != null)
                actionManager.UseAction(1);
        }
    }
}
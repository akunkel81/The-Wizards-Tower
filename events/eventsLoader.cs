using System.IO;
using UnityEngine;

public class EventsLoader : MonoBehaviour
{
    public static EventsLoader Instance { get; private set; }

    public EventData eventData;

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

    public void LoadEventsData()
    {
        string fileName = "events.json"; 
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName); // Looks for "events.json" in Assets/StreamingAssets.

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
           eventData = JsonUtility.FromJson<EventData>(jsonData);

        if (eventData == null || eventData.events == null)
        {
            Debug.LogError("JSON parsed but events array is null. Check JSON format.");
            return;
        }

        Debug.Log("Events data loaded successfully.");
        Debug.Log($"Loaded {eventData.events.Length} events.");
                }
        else
        {
            Debug.LogError("Events data file not found at " + filePath);
        }
    }
}

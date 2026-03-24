using UnityEngine;

public class EventPicker : MonoBehaviour
{
    public EventsLoader eventsLoader;

    private void Awake()
    {
        // Auto fill if not assigned in Inspector
        if (eventsLoader == null)
        {
            eventsLoader = FindFirstObjectByType<EventsLoader>();
        }
    }

    public Event PickRandomEvent()
    {
        if (eventsLoader == null || eventsLoader.eventData == null || eventsLoader.eventData.events == null)
        {
            Debug.LogError("EventPicker: Events not loaded yet or EventsLoader missing.");
            return null;
        }

        var events = eventsLoader.eventData.events;

        if (events.Length == 0)
        {
            Debug.LogError("EventPicker: events array is empty.");
            return null;
        }

        float totalWeight = 0f;
        foreach (var e in events)
        {
            if (e.likelihood > 0f) totalWeight += e.likelihood;
        }

        if (totalWeight <= 0f)
        {
            Debug.LogWarning("EventPicker: totalWeight <= 0.  Returning first event.");
            return events[0];
        }

        float roll = Random.Range(0f, totalWeight);
        
        float running = 0f;
        foreach (var e in events)
        {
            if (e.likelihood <= 0f) continue;

            running += e.likelihood;
            if (roll <= running)
            {
                Debug.Log("Picked event: " + e.name);
                return e;
            }
        }

        return events[events.Length - 1];
    }
}
using System;

// Class for each indicidual event object.
[Serializable]
public class Event
{
    public string name;
    public float likelihood;
    public float sellPriceChange;
    public string[] itemAffectedType; // Array of item types affected by the event (e.g., "Crop", "Mineral").
    public float itemTypeAvail; // Multiplier for the availability of the affected item type during the event.
}

// Class that defines object to hold Array of Events.
[Serializable]
public class EventData
{
    public Event[] events;
}

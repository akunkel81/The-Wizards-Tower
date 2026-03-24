using UnityEngine;
using TMPro;

public class EventsDisplay : MonoBehaviour
{
    public TextMeshProUGUI eventTextbox;

    private void OnEnable()
    {
        GameManager.OnYearEventChosen += UpdateEventText;

        if (GameManager.Instance != null && GameManager.Instance.currentYearEvent != null)
        {
            UpdateEventText(GameManager.Instance.currentYearEvent);
        }
    }

    private void OnDisable()
    {
        GameManager.OnYearEventChosen -= UpdateEventText;
    }

    private void UpdateEventText(Event eventToPrint)
    {
        if (eventTextbox == null)
        {
            Debug.LogError("EventsDisplay: eventTextbox not assigned.");
            return;
        }

        if (eventToPrint == null)
        {
            Debug.LogError("EventsDisplay: eventToPrint is null.");
            return;
        }

        eventTextbox.text =
            "An event has struck the town! : " +
            eventToPrint.name +
            ". It has the following effects: all prices have changed by " +
            eventToPrint.sellPriceChange +
            " and the following item(s) ";

        if (eventToPrint.itemAffectedType != null)
        {
            foreach (var item in eventToPrint.itemAffectedType)
            {
                eventTextbox.text += item + " ";
            }
        }

        eventTextbox.text += " now has an availability of " + eventToPrint.itemTypeAvail;
    }
}
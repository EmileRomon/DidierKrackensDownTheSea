using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _eventNameText;
    [SerializeField] private TextMeshProUGUI _eventDescriptionText;
    [SerializeField] private TextMeshProUGUI _eventOutcomesText;
    [SerializeField] private Transform _optionsContainer;
    [SerializeField] private EventOptionElementController _optionPrefab;

    private LinkedList<Event> eventsToDisplay;

    public void DisplayEvent()
    {
        Event eventToDisplay = eventsToDisplay.First.Value;
        if (eventToDisplay == null)
        {
            gameObject.SetActive(false);
            return;
        }
        eventsToDisplay.RemoveFirst();

        _eventNameText.text = eventToDisplay.EventName;
        _eventDescriptionText.text = eventToDisplay.EventDescription;

        _eventOutcomesText.text = "";
        foreach (EventOutcome outcome in eventToDisplay.EventOutcomes)
        {
            _eventOutcomesText.text += outcome.ToString() + " ";
        }

        foreach (Transform child in _optionsContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (EventOption option in eventToDisplay.EventOptions)
        {
            GameObject optionGO = Instantiate(_optionPrefab.gameObject, _optionsContainer);
            EventOptionElementController optionElm = optionGO.GetComponent<EventOptionElementController>();
            optionElm.OptionDescription = option.OptionDescription;
            optionElm.OptionButton.onClick.AddListener(() => LoadRandomEvent(option.OptionSubEvents, true));
        }
    }

    public void LoadRandomEvent(List<Event> events, bool displayImmediately = false)
    {
        Event e = PickRandomEvent(events);
        if (e != null)
        {
            if (displayImmediately)
            {
                eventsToDisplay.AddFirst(e);
            }
            else
            {
                eventsToDisplay.AddLast(e);
            }
        }
        if (displayImmediately)
        {
            DisplayEvent();
        }
    }

    private Event PickRandomEvent(List<Event> events)
    {
        float sum = 0f;
        foreach (Event e in events)
        {
            sum += e.EventProbability;
        }

        if (sum != 0f)
        {
            //In case the sum of probability is not 1
            float rand = Random.value * sum;

            sum = 0f;
            foreach (Event e in events)
            {
                sum += e.EventProbability;
                if (rand < sum)
                {
                    return e;
                }
            }
        }
        return null;
    }


}

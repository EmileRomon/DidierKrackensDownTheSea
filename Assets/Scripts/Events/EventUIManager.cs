using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _eventNameText;
    [SerializeField] private TextMeshProUGUI _eventDescriptionText;
    [SerializeField] private TextMeshProUGUI _eventOutcomesText;
    [SerializeField] private GameObject _Container;
    [SerializeField] private Transform _optionsContainer;
    [SerializeField] private EventOptionElementController _optionPrefab;

    [SerializeField] private GameController _gameController;

    private class EventTarget
    {
        public Event EventToApply { get; }
        public Boat Target { get; }
        public EventTarget(Event e, Boat b)
        {
            EventToApply = e;
            Target = b;
        }
    }

    private LinkedList<EventTarget> eventsToDisplay = new LinkedList<EventTarget>();

    public void DisplayAllEvents()
    {
        _Container.SetActive(true);
        DisplayEvent();
    }

    private void DisplayEvent()
    {
        if (eventsToDisplay.First == null)
        {
            _Container.SetActive(false);
            return;
        }
        Event eventToDisplay = eventsToDisplay.First.Value.EventToApply;
        Boat target = eventsToDisplay.First.Value.Target;
        eventsToDisplay.RemoveFirst();

        _eventNameText.text = string.Format("{0} -{1}, {2}", eventToDisplay.EventName, target.CurrentZone.Descriptor.ZoneName, target.Descriptor.ItemName);
        _eventDescriptionText.text = eventToDisplay.EventDescription;

        _eventOutcomesText.text = "";
        foreach (EventOutcome outcome in eventToDisplay.EventOutcomes)
        {
            _eventOutcomesText.text += outcome.ToString() + " ";
            _gameController.ApplyOutcomeEffect(outcome, target);
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
            optionElm.OptionButton.onClick.AddListener(() => LoadRandomEvent(option.OptionSubEvents, target, true));
        }
    }

    public Event LoadRandomEvent(List<Event> events, Boat eventTarget, bool displayImmediately = false)
    {
        Event e = PickRandomEvent(events);
        EventTarget et = new EventTarget(e, eventTarget);
        if (e != null)
        {
            if (displayImmediately)
            {
                eventsToDisplay.AddFirst(et);
            }
            else
            {
                eventsToDisplay.AddLast(et);
            }
        }
        if (displayImmediately)
        {
            DisplayEvent();
        }
        return e;
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

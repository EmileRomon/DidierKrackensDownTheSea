using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _eventNameText;
    [SerializeField] private TextMeshProUGUI _eventDescriptionText;
    [SerializeField] private Transform _optionsContainer;
    [SerializeField] private EventOptionElementController _optionPrefab;

    public void DisplayEvent(Event eventToDisplay)
    {
        _eventNameText.text = eventToDisplay.EventName;
        _eventDescriptionText.text = eventToDisplay.EventDescription;

        foreach (Transform child in _optionsContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (EventOption option in eventToDisplay.EventOptions)
        {
            GameObject optionGO = Instantiate(_optionPrefab.gameObject, _optionsContainer);
            EventOptionElementController optionElm = optionGO.GetComponent<EventOptionElementController>();
            optionElm.OptionDescription = option.OptionDescription;
            optionElm.OptionButton.onClick.AddListener(() => LoadRandomEvent(option.OptionSubEvents));
        }
    }

    public void LoadRandomEvent(List<Event> events)
    {
        if (events.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }

        float sum = 0f;
        foreach (Event e in events)
        {
            sum += e.EventProbability;
        }
        //In case the sum of probability is not 1
        float rand = Random.value * sum;

        sum = 0f;
        foreach (Event e in events)
        {
            sum += e.EventProbability;
            if (rand < sum)
            {
                DisplayEvent(e);
                return;
            }
        }
    }
}

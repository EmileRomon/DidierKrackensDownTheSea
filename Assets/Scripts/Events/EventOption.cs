using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventOption", menuName = "Environment/Events/EventOption")]
public class EventOption : ScriptableObject
{
    [SerializeField] private string _optionDescription;
    [SerializeField] private List<Event> _optionSubEvents;

    public string OptionDescription => _optionDescription;
    public List<Event> OptionSubEvents => _optionSubEvents;
}

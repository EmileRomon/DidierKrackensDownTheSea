using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProbableEvent
{
    [SerializeField] private Event _event;
    [Range(0, 1)] [SerializeField] private float _probability;
}

[CreateAssetMenu(fileName = "EventOption", menuName = "Environment/Events/EventOption")]
public class EventOption : ScriptableObject
{
    [SerializeField] private string _optionDescription;
    [SerializeField] private List<ProbableEvent> _optionSubEvents;

    public string OptionDescription => _optionDescription;
    public List<ProbableEvent> OptionSubEvents => _optionSubEvents;
}

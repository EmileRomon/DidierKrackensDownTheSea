using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Environment/Events/Event")]
public class Event : ScriptableObject
{
    [SerializeField] private string _eventName;
    [TextArea] [SerializeField] private string _eventDescription;
    [SerializeField] private List<EventOption> _eventOptions;
    [SerializeField] private List<EventOutcome> _eventOutcomes;
    [Range(0, 1)] [SerializeField] private float _eventProbability;


    public string EventName => _eventName;
    public string EventDescription => _eventDescription;
    public List<EventOption> EventOptions => _eventOptions;
    public List<EventOutcome> EventOutcomes => _eventOutcomes;
    public float EventProbability => _eventProbability;
}

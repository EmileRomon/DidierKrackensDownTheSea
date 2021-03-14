using System.Collections.Generic;
using UnityEngine;

public enum EventOutcome { None, LoseCrewMember, LoseBoat, LoseMoney, WinMoney };

[CreateAssetMenu(fileName = "Event", menuName = "Environment/Events/Event")]
public class Event : ScriptableObject
{
    [SerializeField] private string _eventName;
    [SerializeField] private string _eventDescription;
    [SerializeField] private List<EventOption> _eventOptions;
    [SerializeField] private List<EventOutcome> _eventOutcomes;

    public string EventName => _eventName;
    public string EventDescription => _eventDescription;
    public List<EventOption> EventOptions => _eventOptions;
    public List<EventOutcome> EventOutcomes => _eventOutcomes;

}

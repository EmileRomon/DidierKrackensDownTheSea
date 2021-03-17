using UnityEngine;

public enum ResourceType { Boat, Crew, Money, Health };

[CreateAssetMenu(fileName = "EventOutcome", menuName = "Environment/Events/EventOutcome")]
public class EventOutcome : ScriptableObject
{
    [SerializeField] private ResourceType _targetResource;
    [SerializeField] private int _value;
    [SerializeField] private bool _affectOtherInZone = false;

    public ResourceType TargetResource => _targetResource;
    public int Value => _value;

    public override string ToString()
    {
        return _targetResource switch
        {
            ResourceType.Crew => _value + " crew member",
            ResourceType.Boat => _value + " boat",
            ResourceType.Health => _value + " health",
            ResourceType.Money => "$" + _value,
            _ => "",
        };
    }
}

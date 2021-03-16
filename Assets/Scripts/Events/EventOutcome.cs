using UnityEngine;

public enum ResourceType { Boat, Crew, Money };

[CreateAssetMenu(fileName = "EventOutcome", menuName = "Environment/Events/EventOutcome")]
public class EventOutcome : ScriptableObject
{
    [SerializeField] private ResourceType _targetResource;
    [SerializeField] private int _value;

    public ResourceType TargetResource => _targetResource;
    public int Value => _value;

    public override string ToString()
    {
        return _targetResource switch
        {
            ResourceType.Crew => _value + " crew member",
            ResourceType.Boat => _value + " boat",
            ResourceType.Money => "$" + _value,
            _ => "",
        };
    }
}

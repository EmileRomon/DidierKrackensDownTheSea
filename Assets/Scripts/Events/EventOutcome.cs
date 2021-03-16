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
        string resource = _targetResource switch
        {
            ResourceType.Crew => " crew member",
            ResourceType.Boat => " boat",
            ResourceType.Money => "$",
            _ => "",
        };
        return _value + resource;
    }
}

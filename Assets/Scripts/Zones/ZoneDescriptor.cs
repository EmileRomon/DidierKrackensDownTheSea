using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Zone", menuName = "Environment/Descriptors/Zone")]
public class ZoneDescriptor : ScriptableObject
{
	[SerializeField] private string _name;
	[SerializeField] private float _maxHealth;
	[SerializeField] private float _rentabilityFactor;
	[SerializeField] private float _dangerFactor;
	[SerializeField] private List<Weather> _possibleWeathers;
	[SerializeField] private List<ZoneAttribute> _attributes;
	[SerializeField] private List<Event> _events;
	[SerializeField] private Sprite _zoneBackground;
	[Range(0, 1)] [SerializeField] private float _noEventProbability;
	[SerializeField] private float _ecoFragilityFactor;
	[SerializeField] private Vector2 _naturalDecayRange;

	public string ZoneName => _name;
	public float MaxHealth => _maxHealth;
	public float RentabilityFactor => _rentabilityFactor;
	public float DangerFactor => _dangerFactor;
	public List<Weather> PossibleWeathers => _possibleWeathers;
	public List<ZoneAttribute> Attributes => _attributes;
	public List<Event> Events => _events;
	public Sprite ZoneBackground => _zoneBackground;
	public float NoEventProbability => _noEventProbability;
	public float EcoFragility => _ecoFragilityFactor;
	public Vector2 NaturalDecayRange =>_naturalDecayRange;

	public Weather PickRandomWeather()
	{
		return PossibleWeathers[Random.Range(0, PossibleWeathers.Count)];
	}
}

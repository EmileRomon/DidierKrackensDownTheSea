using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Zone", menuName = "Environment/Descriptors/Zone")]
public class ZoneDescriptor : ScriptableObject
{
	[SerializeField] private float _maxHealth;
	[SerializeField] private float _rentabilityFactor;
	[SerializeField] private float _dangerFactor;
	[SerializeField] private List<Weather> _possibleWeathers;
	[SerializeField] private List<ZoneAttribute> _attributes;
	//UI : montrer temps possibles + celui actuel en surbrillance

	public float MaxHealth => _maxHealth;
	public float RentabilityFactor => _rentabilityFactor;
	public float DangerFactor => _dangerFactor;
	public List<Weather> PossibleWeathers => _possibleWeathers;
	public List<ZoneAttribute> Attributes => _attributes;

	public Weather PickRandomWeather()
	{
		return PossibleWeathers[Random.Range(0, PossibleWeathers.Count)];
	}
}

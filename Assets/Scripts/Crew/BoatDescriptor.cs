using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boat", menuName = "Crew/Descriptor/Boat")]
public class BoatDescriptor : IncomeGenerator
{
	[SerializeField] private float _maxHealth;
	[SerializeField] private int _minCrew;
	[SerializeField] private int _maxCrew;
	[SerializeField] private float _ecoImpactFactor;

	public float MaxHealth => _maxHealth;
	public int MinCrew => _minCrew;
	public int MaxCrew => _maxCrew;
	public float EcoImpactFactor => _ecoImpactFactor;

}

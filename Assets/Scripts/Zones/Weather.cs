using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weather", menuName = "Environment/Weather")]
public class Weather : ScriptableObject
{
	[SerializeField] private string _weatherName;
	public string WeatherName => _weatherName;

	//TO-DO: risk increase, rentability increase, etc.
}

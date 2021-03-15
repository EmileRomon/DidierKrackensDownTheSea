using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weather", menuName = "Environment/Weather")]
public class Weather : ScriptableObject
{
	[SerializeField] private string _weatherName;
	[SerializeField] private Sprite _weatherSprite;
	public string WeatherName => _weatherName;
	public Sprite WeatherSprite => _weatherSprite;

	//TO-DO: risk increase, rentability increase, etc.
}

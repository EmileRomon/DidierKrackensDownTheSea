using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Weather", menuName = "Environment/Weather")]
public class Weather : ScriptableObject
{
	[SerializeField] private string _weatherName;
	[SerializeField] private Sprite _weatherSprite;
	[SerializeField] private float _riskIncrease;
	[SerializeField] private float _profitIncrease;
	public string WeatherName => _weatherName;
	public Sprite WeatherSprite => _weatherSprite;
	public float RiskIncrease => _riskIncrease;
	public float ProfitIncrease => _profitIncrease;
}

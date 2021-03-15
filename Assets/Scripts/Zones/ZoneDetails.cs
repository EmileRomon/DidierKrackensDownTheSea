using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZoneDetails : MonoBehaviour
{
	[SerializeField] private RawImage _preview;
	[SerializeField] private TextMeshProUGUI _name;
	[SerializeField] private TextMeshProUGUI _health;
	[SerializeField] private TextMeshProUGUI _profit;
	[SerializeField] private TextMeshProUGUI _risk;
	[SerializeField] private RectTransform _weatherRoot;

	[SerializeField] private Image _weatherPrefab;
	[SerializeField] private Color _enabledColor;
	[SerializeField] private Color _disabledColor;

	public void UpdateDetails(Zone zone)
	{
		_name.text = zone.name;
		_health.text = string.Format("{0}/{1}", zone.CurrentHealth, zone.Descriptor.MaxHealth);
		_profit.text = zone.Descriptor.RentabilityFactor.ToString();
		_risk.text = zone.Descriptor.DangerFactor.ToString();
		
		foreach(Transform t in _weatherRoot.transform)
		{
			Destroy(t.gameObject);
		}
		foreach (Weather weather in zone.Descriptor.PossibleWeathers)
		{
			Image weatherImage = GameObject.Instantiate(_weatherPrefab, _weatherRoot);
			weatherImage.sprite = weather.WeatherSprite;
			weatherImage.color = (zone.CurrentWeather == weather) ? _enabledColor : _disabledColor;
		}
	}
}
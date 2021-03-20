using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(MenuButtonHandler))]
public class ZoneDetails : MonoBehaviour
{
	[SerializeField] private Image _preview;
	[SerializeField] private TextMeshProUGUI _name;
	[SerializeField] private Image _health;
	[SerializeField] private TextMeshProUGUI _profit;
	[SerializeField] private TextMeshProUGUI _risk;
	[SerializeField] private RectTransform _weatherRoot;

	[SerializeField] private Image _weatherPrefab;
	[SerializeField] private Color _enabledColor;
	[SerializeField] private Color _disabledColor;

	[SerializeField] private RectTransform _boatsRoot;
	[SerializeField] private BoatListItem _boatPrefab;

	[SerializeField] private DragDropReceptor _dragDropReceptor;

	private MenuButtonHandler _menuButtonsHandler;

	private void Awake()
	{
		_menuButtonsHandler = GetComponent<MenuButtonHandler>();	
	}

	public void UpdateDetails(Zone zone)
	{
		_menuButtonsHandler.OnDisplay();
		DragDropReceptor.SetOpenZone(zone);
		_dragDropReceptor.DragDropZone = zone;
		_name.text = zone.Descriptor.ZoneName;
		_health.fillAmount = zone.CurrentHealth / zone.Descriptor.MaxHealth;
		_profit.text = "Profit: " + zone.Descriptor.RentabilityFactor.ToString();
		_risk.text = "Danger: " + zone.Descriptor.DangerFactor.ToString();
		_preview.sprite = zone.Descriptor.ZoneBackground;
		
		foreach(Transform t in _weatherRoot.transform)
		{
			Destroy(t.gameObject);
		}
		foreach (Weather weather in zone.Descriptor.PossibleWeathers)
		{
			Image weatherBackground = GameObject.Instantiate(_weatherPrefab, _weatherRoot);
			Image weatherSprite = weatherBackground.rectTransform.GetChild(0).GetComponent<Image>();
			weatherSprite.sprite = weather.WeatherSprite;
			weatherBackground.color = (zone.CurrentWeather == weather) ? _enabledColor : _disabledColor;
		}

		foreach(Transform t in _boatsRoot.transform)
        {
			Destroy(t.gameObject);
        }
		foreach(Boat b in zone.PlacedBoats)
        {
			BoatListItem db = GameObject.Instantiate(_boatPrefab, _boatsRoot);
			db.SetBoat(b);
        }
	}

	public void OnClose()
	{
		DragDropReceptor.SetOpenZone(null);
	}
}

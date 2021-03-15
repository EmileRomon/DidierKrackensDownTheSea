using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
	[SerializeField] private ZoneDescriptor _descriptor;
	[SerializeField] private ZoneDetails _details;

	public ZoneDescriptor Descriptor => _descriptor;
	public float CurrentHealth { get; set; }
	public Weather CurrentWeather { get; set; }

	private List<Boat> _placedBoats = new List<Boat>();
	public List<Boat> PlacedBoats => _placedBoats;

	private void Awake()
	{
		CurrentHealth = _descriptor.MaxHealth;
		ChangeWeather();
	}

	public void ChangeWeather()
	{
		CurrentWeather = Descriptor.PickRandomWeather();
	}

	public void OpenZoneDetails()
	{
		_details.gameObject.SetActive(true);
		_details.UpdateDetails(this);
	}
}

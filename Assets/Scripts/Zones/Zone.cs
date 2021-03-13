using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
	public ZoneDescriptor Descriptor { get; set; }
	public float CurrentHealth { get; set; }
	public Weather CurrentWeather { get; set; }

	public Zone(ZoneDescriptor descriptor)
	{
		Descriptor = descriptor;
		CurrentHealth = descriptor.MaxHealth;
		ChangeWeather();
	}

	public void ChangeWeather()
	{
		CurrentWeather = Descriptor.PickRandomWeather();
	}
}

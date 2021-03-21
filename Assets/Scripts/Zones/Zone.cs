using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] private ZoneDescriptor _descriptor;
    [SerializeField] private ZoneDetails _details;
	private float _currentDayIncome = 0;

	public float CurrentDayIncome => _currentDayIncome;

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

    public float GetMoney(float price)
    {
		_currentDayIncome = 0;

        foreach (Boat boat in _placedBoats)
        {
			_currentDayIncome += (boat.Descriptor.IncomeFactor * _descriptor.RentabilityFactor);
        }

		_currentDayIncome *= price;
		return _currentDayIncome;


	}

    public void DecayFromBoats()
    {
        foreach (Boat boat in _placedBoats)
        {
            CurrentHealth -= ((BoatDescriptor)boat.Descriptor).EcoImpactFactor * Descriptor.EcoFragility;
        }
    }

    public void DecayNatural()
    {
        float rand = Random.Range(Descriptor.NaturalDecayRange.x, Descriptor.NaturalDecayRange.y);
        CurrentHealth -= rand * Descriptor.EcoFragility;
    }

    public void OpenZoneDetails()
    {
        _details.transform.GetChild(0).gameObject.SetActive(true);
        _details.UpdateDetails(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : CrewItem
{
	public float CurrentHealth { get; set; }
	private List<CrewMember> _crew = new List<CrewMember>();

	public List<CrewMember> Crew => _crew;

	public Zone CurrentZone { get; set; }

	public Boat(BoatDescriptor descriptor)
	{
		Descriptor = descriptor;
		CurrentHealth = descriptor.MaxHealth;
		CurrentZone = null;
	}

	public override float Maintain()
	{
		return Descriptor.MaintenancePrice;
	}

	public override float Sell()
	{
		//TO-DO: Calculer un prix de vente basé sur le prix de vente de base et la vie du bateau.
		return Descriptor.ResalePrice;
	}

	public float Repair()
	{
		//TO-DO: Add a repair price
		return Descriptor.PurchasePrice / 2;
	}

	public void AffectNewZone(Zone zone)
	{
		if (CurrentZone != null)
		{
			CurrentZone.PlacedBoats.Remove(this);
		}
		CurrentZone = zone;
		if (zone != null) zone.PlacedBoats.Add(this);
	}

	public bool CheckAvailable()
	{
		BoatDescriptor bd = (BoatDescriptor)Descriptor;
		return _crew.Count >= bd.MinCrew && _crew.Count <= bd.MaxCrew;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : IMaintainable
{
	public BoatDescriptor Descriptor { get; set; }

	public float CurrentHealth { get; set; }
	public int CrewCount { get; set; }

	public Zone CurrentZone { get; set; }

	public Boat(BoatDescriptor descriptor)
	{
		Descriptor = descriptor;
		CurrentHealth = descriptor.MaxHealth;
		CrewCount = 0;
		CurrentZone = null;
	}

	public float Maintain()
	{
		return Descriptor.MaintenancePrice;
	}

	public float Sell()
	{
		//TO-DO: Calculer un prix de vente basé sur le prix de vente de base et la vie du bateau.
		return Descriptor.SellPrice;
	}
}

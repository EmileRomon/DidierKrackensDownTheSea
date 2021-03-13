using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMember : IMaintainable
{
	public CrewMemberDescriptor Descriptor { get; set; }
	public Boat CurrentBoat { get; set; }

	public CrewMember(CrewMemberDescriptor descriptor)
	{
		Descriptor = descriptor;
		CurrentBoat = null;
	}

	public float Maintain()
	{
		return Descriptor.MaintenancePrice;
	}

	public float Sell()
	{
		return Descriptor.SellPrice;
	}
}

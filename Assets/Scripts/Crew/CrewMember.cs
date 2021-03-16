using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMember : CrewItem
{
	public Boat CurrentBoat { get; set; }

	public CrewMember(CrewMemberDescriptor descriptor)
	{
		Descriptor = descriptor;
		CurrentBoat = null;
	}

	public override float Maintain()
	{
		return Descriptor.MaintenancePrice;
	}

	public override float Sell()
	{
		return Descriptor.ResalePrice;
	}
}

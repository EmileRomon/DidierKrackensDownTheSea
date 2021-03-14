using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewDetails : ItemDetails
{
	public override void UpdateDetails<T>(T itemDescriptor)
	{
		base.UpdateDetails(itemDescriptor);
		CrewMemberDescriptor crewDescriptor = itemDescriptor as CrewMemberDescriptor;
		if (crewDescriptor != null)
		{
			//nothing to do for now
		}
	}
}

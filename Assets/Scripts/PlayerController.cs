using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region Crew
	private List<Boat> _boats;
	private List<CrewMember> _crewMembers;

	public void AddBoat(BoatDescriptor descriptor)
	{
		_boats.Add(new Boat(descriptor));
	}

	public void AddCrewMember(CrewMemberDescriptor descriptor)
	{
		_crewMembers.Add(new CrewMember(descriptor));
	}
	#endregion Crew

	#region Management
	private float _moneyScore;
	private float _moneyAmount;
	private int _currentDay = 1;

	public void PayAllBoatsMaintenance()
	{
		foreach(Boat boat in _boats)
		{
			if(boat.Descriptor.MaintenancePrice <= _moneyAmount)
			{
				boat.Maintain();
			}
			else
			{
				return;
			}
		}
	}

	public void PayAllCrewMembersMaintenance()
	{
		foreach(CrewMember member in _crewMembers)
		{
			if (member.Descriptor.MaintenancePrice <= _moneyAmount)
			{
				member.Maintain();
			}
			else
			{
				return;
			}
		}
	}
	#endregion Money
}

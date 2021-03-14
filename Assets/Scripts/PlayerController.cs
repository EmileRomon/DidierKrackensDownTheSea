using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	#region Crew
	private Dictionary<string, List<Boat>> _boats = new Dictionary<string, List<Boat>>();
	private Dictionary<string, List<CrewMember>> _crewMembers = new Dictionary<string, List<CrewMember>>();

	public void AddBoat(BoatDescriptor descriptor)
	{
		if(_boats.ContainsKey(descriptor.ItemName))
		{
			List<Boat> boats = _boats[descriptor.ItemName];
			boats.Add(new Boat(descriptor));
			_itemsManager.UpdateItem(descriptor, boats.Count);
		}
		else
		{
			List<Boat> boats = new List<Boat>();
			boats.Add(new Boat(descriptor));
			_boats.Add(descriptor.ItemName, boats);
			_itemsManager.CreateItem(descriptor);
		}
	}

	public void AddCrewMember(CrewMemberDescriptor descriptor)
	{
		if (_crewMembers.ContainsKey(descriptor.ItemName))
		{
			List<CrewMember> members = _crewMembers[descriptor.ItemName];
			members.Add(new CrewMember(descriptor));
			_itemsManager.UpdateItem(descriptor, members.Count);
		}
		else
		{
			List<CrewMember> members = new List<CrewMember>();
			members.Add(new CrewMember(descriptor));
			_crewMembers.Add(descriptor.ItemName, members);
			_itemsManager.CreateItem(descriptor);
		}
	}
	#endregion Crew

	#region Management
	[SerializeField] private ItemsManager _itemsManager;
	private float _moneyScore;
	private float _moneyAmount;
	private int _currentDay = 1;

	public void PayAllBoatsMaintenance()
	{
		foreach (KeyValuePair<string, List<Boat>> pair in _boats)
		{
			foreach (Boat boat in pair.Value)
			{
				if (boat.Descriptor.MaintenancePrice <= _moneyAmount)
				{
					boat.Maintain();
				}
				else
				{
					return;
				}
			}
		}
	}

	public void PayAllCrewMembersMaintenance()
	{
		foreach(KeyValuePair<string, List<CrewMember>> pair in _crewMembers)
		{
			foreach (CrewMember member in pair.Value)
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
	}
	#endregion Money

	#region Debug
	public List<BoatDescriptor> _boatDescriptors;
	public List<CrewMemberDescriptor> _crewDescriptors;

	[ContextMenu("Add items")]
	public void AddItems()
	{
		foreach (BoatDescriptor boat in _boatDescriptors) AddBoat(boat);
		foreach (CrewMemberDescriptor crew in _crewDescriptors) AddCrewMember(crew);
	}
	#endregion Debug
}

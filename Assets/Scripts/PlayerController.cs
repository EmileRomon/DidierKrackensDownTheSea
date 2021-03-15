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
		Boat boat = new Boat(descriptor);
		if (_boats.ContainsKey(descriptor.ItemName))
		{
			List<Boat> boats = _boats[descriptor.ItemName];
			boats.Add(boat);
		}
		else
		{
			List<Boat> boats = new List<Boat>();
			boats.Add(boat);
			_boats.Add(descriptor.ItemName, boats);
			
		}
		_itemsManager.CreateBoatItem(boat);
	}

	public void AddCrewMember(CrewMemberDescriptor descriptor)
	{
		CrewMember crewMember = new CrewMember(descriptor);
		if (_crewMembers.ContainsKey(descriptor.ItemName))
		{
			List<CrewMember> members = _crewMembers[descriptor.ItemName];
			members.Add(crewMember);
		}
		else
		{
			List<CrewMember> members = new List<CrewMember>();
			members.Add(crewMember);
			_crewMembers.Add(descriptor.ItemName, members);
		}
		_itemsManager.CreateCrewItem(crewMember);
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

	public void SellItem(ManagementItem mgmtItem)
	{
		float amountToSell = mgmtItem.Item.Sell();
		Debug.Log("Sold for " + amountToSell);
		_moneyAmount += amountToSell;

	}

	public void PurchaseItem(ShopItem shopItem)
	{
		if (shopItem.ItemDescriptor.PurchasePrice <= _moneyAmount)
		{
			_moneyAmount -= shopItem.ItemDescriptor.PurchasePrice;
			Debug.Log("Spent " + shopItem.ItemDescriptor.PurchasePrice + " coins");
			BoatDescriptor boatDescriptor = shopItem.ItemDescriptor as BoatDescriptor;
			if(boatDescriptor != null)
			{
				AddBoat(boatDescriptor);
			}
			else
			{
				AddCrewMember(shopItem.ItemDescriptor as CrewMemberDescriptor);
			}
		}
	}

	public void RepairItem(ManagementBoat mgmtItem)
	{
		Boat boat = mgmtItem.Item as Boat;
		if(boat != null)
		{
			float amountToPay = boat.Repair();
			if (_moneyAmount >= amountToPay)
			{
				Debug.Log("Repaired " + amountToPay);
				_moneyAmount -= amountToPay;
				mgmtItem.UpdateItem();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
	private List<ManagementItem> _items;

	public void CreateItem(IncomeGenerator itemDescriptor)
	{
		//add panel
		Debug.Log("create " + itemDescriptor.ItemName);
	}

	public void DeleteItem(IncomeGenerator itemDescriptor)
	{
		//search and remove panel
	}

	public void UpdateItem(IncomeGenerator itemDescriptor, int amount)
	{
		//search and update panel count
		Debug.Log("update " + itemDescriptor.ItemName + " with " + amount);
	}
}

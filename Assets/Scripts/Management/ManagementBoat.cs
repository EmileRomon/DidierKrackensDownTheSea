using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagementBoat : ManagementItem
{
	[SerializeField] private Button _repairButton;
	[SerializeField] private TextMeshProUGUI _itemRepairPriceText;
	[SerializeField] private Image _health;

	public Button RepairButton => _repairButton;

	public override void InitItem(CrewItem item)
	{
		base.InitItem(item);
		UpdateItem();
		
	}

	public override void UpdateItem()
	{
		Boat boat = _item as Boat;
		if (boat != null)
		{
			BoatDescriptor boatDescriptor = boat.Descriptor as BoatDescriptor;
			_health.fillAmount = boat.CurrentHealth / boatDescriptor.MaxHealth;
			_itemRepairPriceText.text = boatDescriptor.MaintenancePrice.ToString("0");
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class ItemDetails : MonoBehaviour 
{
	[SerializeField] protected Image _preview;
	[SerializeField] protected TextMeshProUGUI _name;
	[SerializeField] protected TextMeshProUGUI _purchasePrice;
	[SerializeField] protected TextMeshProUGUI _maintenancePrice;
	[SerializeField] protected TextMeshProUGUI _resalePrice;
	[SerializeField] protected TextMeshProUGUI _income;

	public virtual void UpdateDetails<T>(T itemDescriptor, float health) where T : IncomeGenerator
	{
		_name.text = itemDescriptor.ItemName;
		_purchasePrice.text = "Purchase: " + itemDescriptor.PurchasePrice.ToString();
		_maintenancePrice.text = "Maintenance: " + itemDescriptor.MaintenancePrice.ToString();
		_resalePrice.text = "Resale: " + itemDescriptor.ResalePrice.ToString();
		_income.text = "Income Factor: " + itemDescriptor.IncomeFactor.ToString();
		_preview.sprite = itemDescriptor.ItemSprite;
	}
}

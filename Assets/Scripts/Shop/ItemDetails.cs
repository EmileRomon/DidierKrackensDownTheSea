using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class ItemDetails : MonoBehaviour 
{
	[SerializeField] protected RawImage _preview;
	[SerializeField] protected TextMeshProUGUI _name;
	[SerializeField] protected TextMeshProUGUI _purchasePrice;
	[SerializeField] protected TextMeshProUGUI _maintenancePrice;
	[SerializeField] protected TextMeshProUGUI _resalePrice;
	[SerializeField] protected TextMeshProUGUI _income;

	public virtual void UpdateDetails<T>(T itemDescriptor) where T : IncomeGenerator
	{
		_name.text = itemDescriptor.ItemName;
		_purchasePrice.text = itemDescriptor.PurchasePrice.ToString();
		_maintenancePrice.text = itemDescriptor.MaintenancePrice.ToString();
		_resalePrice.text = itemDescriptor.ResalePrice.ToString();
		_income.text = itemDescriptor.IncomeFactor.ToString();
	}
}

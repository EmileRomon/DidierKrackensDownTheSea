using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _itemNameText;
	[SerializeField] private Button _detailsButton;
	[SerializeField] private Button _purchaseButton;

	private IncomeGenerator _itemDescriptor;

	public Button DetailsButton => _detailsButton;
	public Button PurchaseButton => _purchaseButton;

	public void Init(string itemName, IncomeGenerator itemDescriptor)
	{
		_itemNameText.text = itemName;
		ItemDescriptor = itemDescriptor;
	}

	public IncomeGenerator ItemDescriptor
	{
		get => _itemDescriptor;
		set
		{
			_itemDescriptor = value;
			_itemNameText.text = ItemDescriptor.ItemName;
		}
	}
}

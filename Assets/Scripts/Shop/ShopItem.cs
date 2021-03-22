using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _itemNameText;
	[SerializeField] private TextMeshProUGUI _itemPriceText;
	[SerializeField] private Button _detailsButton;
	[SerializeField] private Button _purchaseButton;
	[SerializeField] private Image _preview;

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
			_itemPriceText.text = ItemDescriptor.PurchasePrice.ToString("0");
			_preview.sprite = ItemDescriptor.ItemSprite;
		}
	}
}

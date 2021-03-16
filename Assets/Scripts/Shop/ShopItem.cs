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

	public Button DetailsButton => _detailsButton;
	public Button PurchaseButton => _purchaseButton;

	public IncomeGenerator ItemDescriptor { get; set; }
}

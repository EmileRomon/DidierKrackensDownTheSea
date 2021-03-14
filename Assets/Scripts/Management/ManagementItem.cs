using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagementItem : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _itemNameText;
	[SerializeField] private Button _detailsButton;
	[SerializeField] private Button _sellButton;

	public Button DetailsButton => _detailsButton;
	public Button SellButton => _sellButton;

	public IncomeGenerator ItemDescriptor { get; set; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class ManagementItem : MonoBehaviour
{
	[SerializeField] protected TextMeshProUGUI _itemNameText;
	[SerializeField] protected Button _detailsButton;
	[SerializeField] protected Button _sellButton;
	[SerializeField] protected Image _preview;

	protected CrewItem _item;

	public Button DetailsButton => _detailsButton;
	public Button SellButton => _sellButton;

	public CrewItem Item => _item;

	public virtual void InitItem(CrewItem item)
	{
		_item = item;
		_itemNameText.text = item.Descriptor.ItemName;
		_preview.sprite = item.Descriptor.ItemSprite;
	}
}

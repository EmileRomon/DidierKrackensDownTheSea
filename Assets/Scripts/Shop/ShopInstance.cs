using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInstance : MonoBehaviour
{
	[SerializeField] private ShopManager _shopManager;
	[SerializeField] private RectTransform _itemsRoot;
	[SerializeField] private ShopItem _itemPrefab;

	[SerializeField] private BoatDetails _boatDetails;
	[SerializeField] private CrewDetails _crewDetails;

	[SerializeField] private PlayerController _playerController;

	private void Awake()
	{
		ConstructShop();
	}

	private void ConstructShop()
	{
		List<CrewMemberDescriptor> crewMembers = _shopManager.CrewMembers;
		foreach(CrewMemberDescriptor crewMember in crewMembers)
		{
			ShopItem shopItem = Instantiate(_itemPrefab, _itemsRoot);
			shopItem.ItemDescriptor = crewMember;
			shopItem.DetailsButton.onClick.AddListener(() => ShowDetails<CrewMemberDescriptor>(_crewDetails, shopItem));
			shopItem.PurchaseButton.onClick.AddListener(() => _playerController.PurchaseItem(shopItem));
		}

		List<BoatDescriptor> boats = _shopManager.Boats;
		foreach (BoatDescriptor boat in boats)
		{
			ShopItem shopItem = Instantiate(_itemPrefab, _itemsRoot);
			shopItem.ItemDescriptor = boat;
			shopItem.DetailsButton.onClick.AddListener(() => ShowDetails<BoatDescriptor>(_boatDetails, shopItem));
			shopItem.PurchaseButton.onClick.AddListener(() => _playerController.PurchaseItem(shopItem));
		}
	}

	private void ShowDetails<T>(ItemDetails shopDetails, ShopItem item) where T : IncomeGenerator
	{
		shopDetails.gameObject.SetActive(true);
		shopDetails.UpdateDetails(item.ItemDescriptor);
	}
}

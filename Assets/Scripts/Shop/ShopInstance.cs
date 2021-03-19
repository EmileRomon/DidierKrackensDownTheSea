using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInstance : MonoBehaviour
{
	[SerializeField] private ShopManager _shopManager;
	[SerializeField] private RectTransform _boatsRoot;
	[SerializeField] private RectTransform _crewMembersRoot;
	[SerializeField] private ShopItem _boatPrefab;
	[SerializeField] private ShopItem _crewMemberPrefab;

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
			ShopItem shopItem = Instantiate(_crewMemberPrefab, _crewMembersRoot);
			shopItem.ItemDescriptor = crewMember;
			shopItem.DetailsButton.onClick.AddListener(() => ShowDetails<CrewMemberDescriptor>(_crewDetails, shopItem));
			shopItem.PurchaseButton.onClick.AddListener(() => _playerController.PurchaseItem(shopItem));
		}

		List<BoatDescriptor> boats = _shopManager.Boats;
		foreach (BoatDescriptor boat in boats)
		{
			ShopItem shopItem = Instantiate(_boatPrefab, _boatsRoot);
			shopItem.ItemDescriptor = boat;
			shopItem.DetailsButton.onClick.AddListener(() => ShowDetails<BoatDescriptor>(_boatDetails, shopItem));
			shopItem.PurchaseButton.onClick.AddListener(() => _playerController.PurchaseItem(shopItem));
		}
	}

	private void ShowDetails<T>(ItemDetails shopDetails, ShopItem item) where T : IncomeGenerator
	{
		shopDetails.gameObject.SetActive(true);
		shopDetails.UpdateDetails(item.ItemDescriptor, 0);
	}
}

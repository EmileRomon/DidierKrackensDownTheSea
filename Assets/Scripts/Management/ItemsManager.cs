using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
	[SerializeField] private RectTransform _crewRoot;
	[SerializeField] private RectTransform _boatRoot;
	[SerializeField] private ManagementCrew _crewPrefab;
	[SerializeField] private ManagementBoat _boatPrefab;

	[SerializeField] private BoatDetails _boatDetails;
	[SerializeField] private CrewDetails _crewDetails;

	private List<ManagementItem> _items = new List<ManagementItem>();

	[SerializeField] private PlayerController _playerController;

	public void CreateBoatItem(CrewItem item)
	{
		ManagementBoat mgmtItem = Instantiate(_boatPrefab, _boatRoot);
		mgmtItem.InitItem(item);
		mgmtItem.SellButton.onClick.AddListener(() => SellItem(mgmtItem));
		mgmtItem.RepairButton.onClick.AddListener(() => _playerController.RepairItem(mgmtItem));
		mgmtItem.DetailsButton.onClick.AddListener(() => ShowDetails<BoatDescriptor>(_boatDetails, mgmtItem));
		_items.Add(mgmtItem);
	}

	public void CreateCrewItem(CrewItem item)
	{
		ManagementCrew mgmtItem = Instantiate(_crewPrefab, _crewRoot);
		mgmtItem.InitItem(item);
		mgmtItem.SellButton.onClick.AddListener(() => SellItem(mgmtItem));
		mgmtItem.DetailsButton.onClick.AddListener(() => ShowDetails<CrewMemberDescriptor>(_crewDetails, mgmtItem));
		_items.Add(mgmtItem);
	}

	public void DeleteItem(CrewItem item)
	{
		foreach(ManagementItem mgmtItem in _items)
		{
			if(mgmtItem.Item == item)
			{
				_items.Remove(mgmtItem);
				Destroy(mgmtItem.gameObject);
				break;
			}
		}
	}

    public void OnEnable()
    {
		foreach (ManagementItem mgmtItem in _items)
			mgmtItem.UpdateItem();
    }

    private void SellItem(ManagementItem item)
	{
		_playerController.SellItem(item);
		_items.Remove(item);
		Destroy(item.gameObject);
	}

	private void ShowDetails<T>(ItemDetails mgmtDetails, ManagementItem item) where T : IncomeGenerator
	{
        if (item.Item is Boat boat)
        {
            mgmtDetails.gameObject.SetActive(true);
            mgmtDetails.UpdateDetails(item.Item.Descriptor, boat.CurrentHealth);
        }
		else if(item.Item is CrewMember)
        {
			mgmtDetails.gameObject.SetActive(true);
			mgmtDetails.UpdateDetails(item.Item.Descriptor, 0f);
        }
	}
}

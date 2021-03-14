using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopBoatDetails : ShopItemDetails
{
	[SerializeField] private TextMeshProUGUI _health;
	[SerializeField] private TextMeshProUGUI _minCrew;
	[SerializeField] private TextMeshProUGUI _maxCrew;
	[SerializeField] private TextMeshProUGUI _ecoImpact;

	public override void UpdateDetails<T>(T itemDescriptor)
	{
		base.UpdateDetails(itemDescriptor);
		BoatDescriptor boatDescriptor = itemDescriptor as BoatDescriptor;
		if (boatDescriptor != null)
		{
			_health.text = boatDescriptor.MaxHealth.ToString();
			_minCrew.text = boatDescriptor.MinCrew.ToString();
			_maxCrew.text = boatDescriptor.MaxCrew.ToString();
			_ecoImpact.text = boatDescriptor.EcoImpactFactor.ToString();
		}
	}
}

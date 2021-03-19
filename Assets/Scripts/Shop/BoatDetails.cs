using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoatDetails : ItemDetails
{
	[SerializeField] private TextMeshProUGUI _health;
	[SerializeField] private TextMeshProUGUI _minCrew;
	[SerializeField] private TextMeshProUGUI _maxCrew;
	[SerializeField] private TextMeshProUGUI _ecoImpact;

	public override void UpdateDetails<T>(T itemDescriptor, float health)
	{
		base.UpdateDetails(itemDescriptor, health);
		BoatDescriptor boatDescriptor = itemDescriptor as BoatDescriptor;
		if (boatDescriptor != null)
		{
			if(health != 0)
			{
				_health.text = string.Format("Health: {0}/{1}", health, boatDescriptor.MaxHealth.ToString());
			}
			else
			{
				_health.text = string.Format("Health: {0}", boatDescriptor.MaxHealth.ToString());
			}
			_minCrew.text = "Min. crew: " + boatDescriptor.MinCrew.ToString();
			_maxCrew.text = "Max. crew: " + boatDescriptor.MaxCrew.ToString();
			_ecoImpact.text = "Eco. Impact: " + boatDescriptor.EcoImpactFactor.ToString();
		}
	}
}

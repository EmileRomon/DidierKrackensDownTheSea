using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndDayRecap : MonoBehaviour
{
	[SerializeField] private GameController _gameController;
	[SerializeField] private MenuButtonHandler _menuButtonHandler;
	private PlayerController _player;


	#region Income
	private float _beginDayIncome;
	[SerializeField] private TextMeshProUGUI _dayTitle;
	[SerializeField] private TextMeshProUGUI _incomeRecap;
	private string _incomeGainStr = "Today, you made ${0}";
	private string _incomeWasteStr = "Today, you wasted ${0}";
	#endregion Income

	#region Zones
	[SerializeField] private TextMeshProUGUI _zoneRecap;
	private string _zoneIncomeStr = "${0} made in {1}.\n";
	private string _zoneRemovedStr = "This zone has been destroyed.";
	#endregion Zones

	private void Awake()
	{
		_player = _gameController.Player;
	}

	public void StartDay()
	{
		_beginDayIncome = _gameController.Player.MoneyAmount;
	}

	public void EndDay()
	{
		_dayTitle.text = string.Format("Day {0}", (_player.CurrentDay - 1).ToString());
		if(_player.MoneyAmount >= _beginDayIncome)
		{
			_incomeRecap.text = string.Format(_incomeGainStr, ((int)(_player.MoneyAmount - _beginDayIncome)));
		}
		else
		{
			_incomeRecap.text = string.Format(_incomeWasteStr, ((int)(_player.MoneyAmount - _beginDayIncome)));
		}

		_zoneRecap.text = "";
		foreach(Zone zone in _gameController.Zones)
		{
			if(zone.CurrentDayIncome > 0)
			{
				_zoneRecap.text += string.Format(_zoneIncomeStr, zone.CurrentDayIncome.ToString(), zone.Descriptor.ZoneName);
			}
		}
		foreach(Zone zone in _gameController.ZonesRemoved)
		{
			if (zone.CurrentDayIncome > 0)
			{
				_zoneRecap.text += string.Format(_zoneIncomeStr, zone.CurrentDayIncome.ToString(), zone.Descriptor.ZoneName);
			}
			_zoneRecap.text += _zoneRemovedStr + "\n";
		}

		_menuButtonHandler.OnDisplay();
	}
}

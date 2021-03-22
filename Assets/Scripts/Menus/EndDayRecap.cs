using TMPro;
using UnityEngine;

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

    #region Outcome
    [SerializeField] private TextMeshProUGUI _salaryRecap;
    private string _salaryStr = "Salaries: $-{0}";
    [SerializeField] private TextMeshProUGUI _deptRecap;
    private string _debtStr = "Debt refund: $-{0}";
    #endregion Outcome

    #region Zones
    [SerializeField] private TextMeshProUGUI _zoneRecap;
    private string _zoneIncomeStr = "${0} made in {1}.\n";
    private string _zoneRemovedStr = "This zone has been destroyed.";
    #endregion Zones

    #region MiniGame
    [SerializeField] private TextMeshProUGUI _miniGameResult;
    private string _miniGameStr = "Personal fishing score: ${0}";
    #endregion MiniGame

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
        if (_player.MoneyAmount >= _beginDayIncome)
        {
            _incomeRecap.text = string.Format(_incomeGainStr, ((int)(_player.MoneyAmount - _beginDayIncome)));
        }
        else
        {
            _incomeRecap.text = string.Format(_incomeWasteStr, ((int)(_player.MoneyAmount - _beginDayIncome)));
        }

        _salaryRecap.text = string.Format(_salaryStr, _gameController.Salaries.ToString("0"));
        _deptRecap.text = string.Format(_debtStr, _gameController.DailyDebt.ToString("0"));

        _zoneRecap.text = "";
        foreach (Zone zone in _gameController.Zones)
        {
            if (zone.CurrentDayIncome > 0)
            {
                _zoneRecap.text += string.Format(_zoneIncomeStr, zone.CurrentDayIncome.ToString(), zone.Descriptor.ZoneName);
            }
        }
        foreach (Zone zone in _gameController.ZonesRemoved)
        {
            if (zone.CurrentDayIncome > 0)
            {
                _zoneRecap.text += string.Format(_zoneIncomeStr, zone.CurrentDayIncome.ToString(), zone.Descriptor.ZoneName);
            }
            _zoneRecap.text += _zoneRemovedStr + "\n";
        }

        if (_gameController.DidPlayMiniGame)
        {
            _miniGameResult.text = string.Format(_miniGameStr, _gameController.MiniGameMoney);
        }
        else
        {
            _miniGameResult.text = "";
        }

        _menuButtonHandler.OnDisplay();
    }
}

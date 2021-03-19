using TMPro;
using UnityEngine;

public class ScoresUIManager : MonoBehaviour
{
	[SerializeField] private GameInfo _gameInfo;
    [SerializeField] private TextMeshProUGUI _dayDisplay;
    [SerializeField] private TextMeshProUGUI _moneyDisplay;
    [SerializeField] private TextMeshProUGUI _scoreDisplay;
	[SerializeField] private TextMeshProUGUI _companyName;

	private void Awake()
	{
		_companyName.text = string.Format("Didier Kraken\n{0}", _gameInfo.CompanyName);
	}

	public void UpdateDaysCounter(int days)
    {
        _dayDisplay.text = string.Format("Day {0}", days);
    }
    public void UpdateMoneyCounter(float amount)
    {
        _moneyDisplay.text = string.Format("{0}", amount);
    }
    public void UpdateScoreCounter(float score)
    {
        _scoreDisplay.text = string.Format("{0}", score);
    }
}

using TMPro;
using UnityEngine;

public class ScoresUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dayDisplay;
    [SerializeField] private TextMeshProUGUI _moneyDisplay;
    [SerializeField] private TextMeshProUGUI _scoreDisplay;

    public void UpdateDaysCounter(int days)
    {
        _dayDisplay.text = string.Format("Days: {0}", days);
    }
    public void UpdateMoneyCounter(float amount)
    {
        _moneyDisplay.text = string.Format("Money: {0}$", amount);
    }
    public void UpdateScoreCounter(float score)
    {
        _scoreDisplay.text = string.Format("Score: {0}", score);
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameOverType { NoMoneyLeft, NoZoneleft };

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreDisplay;
    [SerializeField] private TextMeshProUGUI _dayDisplay;
    [SerializeField] private TextMeshProUGUI _reasonDisplay;

    public void DisplayGameOver(float score, int days, GameOverType gameOverType)
    {
        _reasonDisplay.text = gameOverType switch
        {
            GameOverType.NoMoneyLeft => "You have no money left.",
            GameOverType.NoZoneleft => "There is no fishing zone left.",
            _ => "",
        };
        _scoreDisplay.text = string.Format("Total score: {0}", score);
        _dayDisplay.text = string.Format("Total days: {0}", days);
    }

    public void ReturnToMainScreen()
    {
        SceneManager.LoadScene("MainScreenScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreDisplay;
    [SerializeField] private TextMeshProUGUI _dayDisplay;
    [SerializeField] private PlayerController _playerController;

    public void DisplayGameOver(float score, int days)
    {
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

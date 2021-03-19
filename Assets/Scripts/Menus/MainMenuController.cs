using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
	[SerializeField] private GameInfo _gameInfo;
	[SerializeField] private TMP_InputField _companyNameIF;
	[SerializeField] private AudioSource _playSound;

	public void StartGame()
	{
		if(_companyNameIF.text.Length > 0 && _companyNameIF.text.Length < 20)
		{
			_gameInfo.CompanyName = _companyNameIF.text;
			_playSound.Play();
			SceneManager.LoadScene("GameScene_TmpQuentin");
		}
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}

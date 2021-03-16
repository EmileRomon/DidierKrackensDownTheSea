using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
	private void Start()
	{
		AudioListener.volume = 0.5f;
	}

	public void StartGame()
	{
		SceneManager.LoadScene("GameScene");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}

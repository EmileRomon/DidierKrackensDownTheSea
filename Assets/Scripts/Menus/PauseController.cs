using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseController : MonoBehaviour
{
	[SerializeField] private RectTransform _pauseContainer;

	public void LoadMainMenu()
	{
		SceneManager.LoadScene("MainScreenScene");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.P))
		{
			_pauseContainer.gameObject.SetActive(!_pauseContainer.gameObject.activeSelf);
		}
	}
}

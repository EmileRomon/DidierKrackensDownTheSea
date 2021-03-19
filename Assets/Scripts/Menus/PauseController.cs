using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseController : MonoBehaviour
{
	[SerializeField] private RectTransform _pauseContainer;
	[SerializeField] private AudioMixerSnapshot _pausedSnapshot;
	[SerializeField] private AudioMixerSnapshot _unpausedSnapshot;

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
			if(_pauseContainer.gameObject.activeSelf)
			{
				Unpause();
			}
			else
			{
				Pause();
			}
			
		}
	}

	private void Pause()
	{
		_pauseContainer.gameObject.SetActive(true);
		_pausedSnapshot.TransitionTo(0.01f);
	}

	private void Unpause()
	{
		_pauseContainer.gameObject.SetActive(false);
		_unpausedSnapshot.TransitionTo(0.01f);
	}
}

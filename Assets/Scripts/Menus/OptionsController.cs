using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsController : MonoBehaviour
{
	private Resolution[] _resolutions;
	[SerializeField] private Button[] _previousButtons;
	[SerializeField] private Button[] _nextButtons;
	[SerializeField] private AudioMixer _audioMixer;

	#region UI
	[SerializeField] private Slider _musicSlider = null;
	[SerializeField] private TextMeshProUGUI _musicSliderText = null;
	[SerializeField] private Slider _soundSlider = null;
	[SerializeField] private TextMeshProUGUI _soundSliderText = null;
	[SerializeField] private Toggle _fullscreen = null;
	[SerializeField] private Toggle _windowed = null;
	[SerializeField] private TMP_Dropdown _resolutionsDD = null;
	#endregion

	private void Awake()
	{
		_resolutions = Screen.resolutions;
		_fullscreen.isOn = Screen.fullScreen;
		_windowed.isOn = !Screen.fullScreen;
	}

	private void Start()
	{
		PopulateResolutions();

		float volume;
		_audioMixer.GetFloat("musicVolume", out volume);
		_musicSlider.value = Mathf.Pow(10, volume / 20);
		_audioMixer.GetFloat("soundVolume", out volume);
		_soundSlider.value = Mathf.Pow(10, volume / 20);

		Screen.fullScreen = true;
	}

	private void PopulateResolutions()
	{
		_resolutionsDD.ClearOptions();

		List<string> resolutionsStr = new List<string>();

		int screenResolutionIndex = 0;
		int i = 0;
		foreach (Resolution resolution in _resolutions)
		{
			string resolutionStr = resolution.width + " x " + resolution.height;
			resolutionsStr.Add(resolutionStr);

			if (resolution.Equals(Screen.currentResolution))
			{
				screenResolutionIndex = i;
			}
			++i;
		}

		_resolutionsDD.AddOptions(resolutionsStr);
		_resolutionsDD.value = screenResolutionIndex;
		_resolutionsDD.RefreshShownValue();
	}

	public void UpdateMusicAudio(float value)
	{
		float volume = Mathf.Log10(value) * 20;
		_audioMixer.SetFloat("musicVolume", volume);
		_musicSliderText.text = "Music Volume: " + ((int)(value * 100)).ToString();
	}

	public void UpdateSoundAudio(float value)
	{
		float volume = Mathf.Log10(value) * 20;
		_audioMixer.SetFloat("soundVolume", volume);
		_soundSliderText.text = "SFX Volume: " + ((int)(value * 100)).ToString();
	}

	public void UpdateResolution(int resolutionIndex)
	{
		Resolution resolution = _resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}

	public void SetGraphicsQuality(int index)
	{
		QualitySettings.SetQualityLevel(index);
	}

	public void SetFullScreen(bool fullscreen)
	{
		Screen.fullScreen = fullscreen;
	}

	public void OnLeftTrigger()
	{
		foreach (Button btn in _previousButtons)
		{
			if (btn.gameObject.activeInHierarchy)
			{
				btn.onClick.Invoke();
				return;
			}
		}
	}

	public void OnRightTrigger()
	{
		foreach (Button btn in _nextButtons)
		{
			if (btn.gameObject.activeInHierarchy)
			{
				btn.onClick.Invoke();
				return;
			}
		}
	}
}

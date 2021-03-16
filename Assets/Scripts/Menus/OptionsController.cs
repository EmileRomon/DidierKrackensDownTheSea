using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
	private Resolution[] _resolutions;
	[SerializeField] private Button[] _previousButtons;
	[SerializeField] private Button[] _nextButtons;

	#region UI
	[SerializeField] private Slider _slider = null;
	[SerializeField] private TextMeshProUGUI _sliderText = null;
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
		UpdateAudio(AudioListener.volume * 100);
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

	public void UpdateAudio(float value)
	{
		AudioListener.volume = value / 100.0f;
		_slider.value = value;
		_sliderText.text = "Volume: " + ((int)value).ToString();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SeagullsSound : MonoBehaviour
{
	private AudioSource _audioSource;
	[SerializeField] private List<AudioClip> _clips;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
		StartCoroutine(TriggerAudioPlay());
	}

	private IEnumerator TriggerAudioPlay()
	{
		while(true)
		{
			float waitingTime = Random.Range(15, 30);
			yield return new WaitForSeconds(waitingTime);
			PlaySound();
		}
	}

	[ContextMenu("PlaySound")]
	public void PlaySound()
	{
		_audioSource.Stop();
		AudioClip clip = _clips[Random.Range(0, _clips.Count)];
		_audioSource.clip = clip;
		_audioSource.Play();
	}
}

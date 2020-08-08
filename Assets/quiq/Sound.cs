using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Sound", menuName = "Custom/Sound")]

public class Sound : ScriptableObject
{

	public string name;

	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume = .75f;
	[Range(0f, 1f)]
	public float volumeVariance = .1f;

	[Range(.1f, 3f)]
	public float pitch = 1f;
	[Range(0f, 1f)]
	public float pitchVariance = .1f;

	public bool loop = false;

	public AudioMixerGroup mixerGroup;

	[HideInInspector]
	public AudioSource source;
	public float time_when_played = -1;
}

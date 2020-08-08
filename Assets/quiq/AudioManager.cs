using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;
public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public List<Sound> sounds = new List<Sound>();

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.time_when_played = -1;
			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}
    public void AddSound(Sound s)
    {
		sounds.Add(s);
		s.source = gameObject.AddComponent<AudioSource>();
		s.source.clip = s.clip;
		s.source.loop = s.loop;
		s.time_when_played = -1;

		s.source.outputAudioMixerGroup = mixerGroup;
	}

    public void Play(string sound)
	{
		
		Sound s = sounds.Find( item => item.name == sound);
		if (s == null)
		{
			
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		//if (s.time_when_played + s.clip.length <= Time.time)
		{
			s.time_when_played = Time.time;
			s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
			s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

			s.source.Play();
		}
	}
	public void Play(Sound s)
	{

		if (!sounds.Contains(s))
		{
			AddSound(s);
			
		}
		{
			s.time_when_played = Time.time;
			s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
			s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

			s.source.Play();
		}
	}

}

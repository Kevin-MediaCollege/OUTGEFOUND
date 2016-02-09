using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;
using Holoville.HOTween.Core.Easing;
using Holoville.HOTween.Plugins.Core;

public class AudioManager2D : MonoBehaviour 
{
	//---------------------------------------------------------------//
	public List<AudioClip> clips = new List<AudioClip>();
	//---------------------------------------------------------------//

	private static AudioManager2D instance;
	public static AudioManager2D Instance { get { return instance; } }

	private List<AudioSource> audioSources = new List<AudioSource>();

	void Awake()
	{
		if (instance == null)
			instance = this;
		else
			return;
	}

	void Start()
	{
		StartCoroutine(updateSounds());
	}

	public void PlaySound(string _name, float _delay = 0, bool _loop = false, float _volume = 0.5f, bool _ignorePause = false)
	{
		foreach (AudioClip clip in clips)
		{
			if (clip.name == _name)
			{
				AudioSource src = gameObject.AddComponent<AudioSource>();
				src.clip = clip;
				src.loop = _loop;
				src.playOnAwake = false;
				src.volume = _volume;
				src.ignoreListenerPause = _ignorePause;

				if (_delay != 0)
					src.PlayDelayed(_delay);
				else
					src.Play();

				audioSources.Add(src);
				return;
			}
		}

		print("could not find audiofile: " + _name);
	}

	public void StopSound(string _name, bool _allInstances = true)
	{
		for (int i = audioSources.Count - 1; i > -1; i--)
		{
			if (audioSources[i].clip.name == _name)
			{
				Destroy(audioSources[i]);
				audioSources.RemoveAt(i);

				if (!_allInstances)
					return;
			}
		}
	}

	private IEnumerator StopSoundDelayed(AudioSource audio, float delay)
	{
		yield return new WaitForSeconds(delay);

		for (int i = audioSources.Count - 1; i > -1; i--)
		{
			if (audioSources[i].clip == audio)
			{
				Destroy(audioSources[i]);
				audioSources.RemoveAt(i);
				break;
			}
		}
	}

	public bool IsPlaying(string _name)
	{
		foreach(AudioSource audio in getSoundsByName(_name, true))
		{
			return true;
		}

		return false;
	}

	private IEnumerator updateSounds()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.1f);
			for (int i = audioSources.Count - 1; i > -1; i--)
			{
				if (!audioSources[i].isPlaying && !audioSources[i].loop)
				{
					Destroy(audioSources[i]);
					audioSources.RemoveAt(i);
				}
			}
		}
	}

	public void PauseSound(string _name, bool _allInstances = true)
	{
		foreach(AudioSource audio in getSoundsByName(_name, _allInstances))
		{
			audio.Pause();
		}
	}

	public void UnPauseSound(string _name, bool _allInstances = true)
	{
		foreach(AudioSource audio in getSoundsByName(_name, _allInstances))
		{
			audio.Play();
		}
	}

	public void FadeTo(string _name, float _time, float _volume, bool _allInstances = true, EaseType _easetype = Holoville.HOTween.EaseType.Linear)
	{
		foreach(AudioSource audio in getSoundsByName(_name, _allInstances))
		{
			HOTween.To (audio, _time, new TweenParms ().Prop ("volume", new PlugFloat(_volume)).Ease(_easetype));
		}
	}

	public void FadeOut(string _name, float _time, bool _allInstances = true, EaseType _easetype = Holoville.HOTween.EaseType.Linear)
	{
		foreach(AudioSource audio in getSoundsByName(_name, _allInstances))
		{
			StartCoroutine(StopSoundDelayed(audio, _time));
			HOTween.To (audio, _time, new TweenParms ().Prop ("volume", new PlugFloat(0f)).Ease(_easetype));
		}
	}

	public void PauseAll()
	{
		AudioListener.pause = true;
	}

	public void UnPauseAll()
	{
		AudioListener.pause = false;
	}

	public List<AudioSource> getSoundsByName(string _name, bool _allInstances)
	{
		List<AudioSource> audioList = new List<AudioSource>();
		for (int i = audioSources.Count - 1; i > -1; i--)
		{
			if (audioSources[i].clip.name == _name)
			{
				audioList.Add(audioSources[i]);
				if (!_allInstances)
				{
					return audioList;
				}
			}
		}
		return audioList;
	}
}

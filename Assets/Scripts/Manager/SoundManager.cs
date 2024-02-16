using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
	private AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.Max];
	Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

	GameObject _soundRoot = null;

	public void Init()
	{
		if (_soundRoot == null)
		{
			_soundRoot = GameObject.Find("@SoundRoot");
			if (_soundRoot == null)
			{
				_soundRoot = new GameObject { name = "@SoundRoot" };
				Object.DontDestroyOnLoad(_soundRoot);

				string[] soundTypeNames = System.Enum.GetNames(typeof(Define.Sound));
				// Sound로 지정되어 있는 목록들을 audioSources 배열에 저장함
				for (int count = 0; count < soundTypeNames.Length - 1; count++)
				{
					GameObject go = new GameObject { name = soundTypeNames[count] };
					_audioSources[count] = go.AddComponent<AudioSource>();
					go.transform.parent = _soundRoot.transform;
				}
				_audioSources[(int)Define.Sound.Bgm].loop = true;
			}
		}
	}

	public bool Play(Define.Sound soundType, string path, float volume = 1.0f, float pitch = 1.0f)
	{
		if (string.IsNullOrEmpty(path))
			return false;

		AudioSource audioSource = _audioSources[(int)soundType];
		if (path.Contains("Sound/") == false)
			path = string.Format("Sound/{0}", path);

		audioSource.volume = volume;

		// BGM 재생
		if (soundType == Define.Sound.Bgm)
		{
			AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
			if (audioClip == null)
				return false;
			
			// 이미 AudioSource가 플레이 중이면 멈추고 해당 BGM 재생
			if (audioSource.isPlaying)
				audioSource.Stop();

			audioSource.clip = audioClip;
			audioSource.pitch = pitch;
			audioSource.Play();
			return true;
		}
		// Effect 재생
		else if (soundType == Define.Sound.Effect)
		{
			AudioClip audioClip = GetAudioClip(path);
			if (audioClip == null)
				return false;
			
			// BGM과 같이 재생되게 함
			audioSource.pitch = pitch;
			audioSource.PlayOneShot(audioClip);
			return true;
		}
		return false;
	}
	
	/// <summary>
	/// Effect의 경우 첫 재생시 Dictionary에 저장해놓고 계속 씀
	/// </summary>
	/// <param name="path"></param>
	/// <returns></returns>
	private AudioClip GetAudioClip(string path)
	{
		AudioClip audioClip = null;
		if (_audioClips.TryGetValue(path, out audioClip))
			return audioClip;

		audioClip = Managers.Resource.Load<AudioClip>(path);
		_audioClips.Add(path, audioClip);
		return audioClip;
	}
}

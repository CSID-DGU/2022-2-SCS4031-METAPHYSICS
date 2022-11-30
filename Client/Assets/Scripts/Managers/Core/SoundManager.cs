using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public float m_MasterSoundVolume = 100.0f;
    public float m_EffectSoundVolume = 100.0f;

    public float MasterSoundVolume
    {
        get
        {
            return m_MasterSoundVolume;
        }

        set
        {
            if (value > 100.0f)
                value = 100.0f;

            else if (value < 0.0f)
                value = 0.0f;

            m_MasterSoundVolume = value;
            UpdateSoundsVolume();
        }
    }

    public float EffectSoundVolume
    {
        get
        {
            return m_EffectSoundVolume;
        }

        set
        {
            if (value > 100.0f)
                value = 100.0f;

            else if (value < 0.0f)
                value = 0.0f;

            m_EffectSoundVolume = value;
            UpdateSoundsVolume();
        }
    }

    // MP3 Player   -> AudioSource
    // MP3 음원     -> AudioClip
    // 관객(귀)     -> AudioListener

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;

                _audioSources[i].volume = m_MasterSoundVolume / 100.0f;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true;
            _audioSources[(int)Define.Sound.LoopEffect].loop = true;
        }

        AudioClip[] BGMs = Resources.LoadAll<AudioClip>("Sounds\\BGM");

        for (int i = 0; i < BGMs.Length; ++i) 
        {
            _audioClips.Add(BGMs[i].name, BGMs[i]);

        }

        AudioClip[] Effects = Resources.LoadAll<AudioClip>("Sounds\\EFFECT");

        for (int i = 0; i < Effects.Length; ++i)
        {
            _audioClips.Add(Effects[i].name, Effects[i]);

        }

        AudioClip[] UIEffects = Resources.LoadAll<AudioClip>("Sounds\\UI");

        for (int i = 0; i < UIEffects.Length; ++i)
        {
            _audioClips.Add(UIEffects[i].name, UIEffects[i]);

        }

        AudioClip[] GameEffects = Resources.LoadAll<AudioClip>("Sounds\\GAME");

        for (int i = 0; i < GameEffects.Length; ++i)
        {
            _audioClips.Add(GameEffects[i].name, GameEffects[i]);

        }
    }

    public void Update()
    {
        //UI 충돌 중일 때만 마우스 클릭 사운드 들리게
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonUp(0))
                PlayByName("Button_Click_1", Define.Sound.UIEffect);
        }
    }

    public void Clear()
    {
        //foreach (AudioSource audioSource in _audioSources)
        //{
        //    audioSource.clip = null;
        //    audioSource.Stop();
        //}
        //_audioClips.Clear();
    }

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    public void PlayByName(string ClipName, Define.Sound Type, float pitch = 1.0f)
    {
        AudioClip Clip = _audioClips[ClipName];

        Play(Clip, Type, pitch);
    }

	public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
	{
        if (audioClip == null)
            return;

		if (type == Define.Sound.Bgm || type == Define.Sound.LoopEffect)
		{
			AudioSource audioSource = _audioSources[(int)type];
			if (audioSource.isPlaying)
				audioSource.Stop();

			audioSource.pitch = pitch;
			audioSource.clip = audioClip;
			audioSource.Play();
		}
		else
		{
			AudioSource audioSource = _audioSources[(int)type];
			audioSource.pitch = pitch;
			audioSource.PlayOneShot(audioClip);
		}
	}

	AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
		if (path.Contains("Sounds/") == false)
			path = $"Sounds/{path}";

		AudioClip audioClip = null;

		if (type == Define.Sound.Bgm)
		{
			audioClip = Managers.Resource.Load<AudioClip>(path);
		}
		else
		{
			if (_audioClips.TryGetValue(path, out audioClip) == false)
			{
				audioClip = Managers.Resource.Load<AudioClip>(path);
				_audioClips.Add(path, audioClip);
			}
		}

		if (audioClip == null)
			Debug.Log($"AudioClip Missing ! {path}");

		return audioClip;
    }

    public AudioClip FindClip(string ClipName)
    {
        return _audioClips[ClipName];
    }
    public AudioSource GetAudioSource(Define.Sound SoundType)
    {
        return _audioSources[(int)SoundType];
    }

    void UpdateSoundsVolume()
    {
        _audioSources[(int)Define.Sound.Bgm].volume = m_MasterSoundVolume / 100.0f;
        _audioSources[(int)Define.Sound.Effect].volume = m_EffectSoundVolume / 100.0f;
        _audioSources[(int)Define.Sound.LoopEffect].volume = m_EffectSoundVolume / 100.0f;
        _audioSources[(int)Define.Sound.UIEffect].volume = m_EffectSoundVolume / 100.0f;
    }
}

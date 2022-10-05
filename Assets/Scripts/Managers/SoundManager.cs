using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum ENUM_SOUND
{
    Success,
    Failed,
    ManaGain,
    SpellCast,
    ShowRuneSymbol,
    HideRuneSymbol,
    DialogueLetter
}

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _masterMixer;

    [Header("Audio Sources")]
    [SerializeField]
    private AudioSource _backgroundMusic;
    [SerializeField]
    private AudioSource _playerClick;
    [SerializeField]
    private AudioSource _soundFX;

    [Header("GameStart Audio")]
    [SerializeField] private AudioClip _backgroundMusicAudio;

    [Header("Volume Sliders")]
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    [Space(10)]  
    [SerializeField] private List<Sound> _gameSounds = new List<Sound>();

    private const string SFX_Volume_KEY = "SoundFX";
    private const string Music_Volume_KEY = "BackgroundMusic";

    private void Start()
    {
        _sfxSlider.value = PlayerPrefs.GetFloat(SFX_Volume_KEY, 1);
       _musicSlider.value = PlayerPrefs.GetFloat(Music_Volume_KEY, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _playerClick.Play();
        } 
    }

    public void StartGameAudio()
    {
        _backgroundMusic.Stop();
        _backgroundMusic.clip = _backgroundMusicAudio;
        _backgroundMusic.volume = 0.7f;
        _backgroundMusic.Play();
    }

    public void PlaySoundEffect(ENUM_SOUND soundToPlay)
    {
        _soundFX.clip = GetClipFromEnum(soundToPlay);
        _soundFX.volume = GetVolumeFromEnum(soundToPlay);

        if (_soundFX.clip)
            _soundFX.Play();
    }

    private AudioClip GetClipFromEnum(ENUM_SOUND sound)
    {
        for (int i = 0; i < _gameSounds.Count; i++)
        {
            if (_gameSounds[i].sound == sound)
                return _gameSounds[i].audioClip;
        }

        return null;
    }

    private float GetVolumeFromEnum(ENUM_SOUND sound)
    {
        for (int i = 0; i < _gameSounds.Count; i++)
        {
            if (_gameSounds[i].sound == sound)
                return _gameSounds[i].volume;
        }

        return 0;
    }

    public void SetBackgroundMusicLevel(float sliderValue)
    {   
        _masterMixer.SetFloat("BackgroundMusic", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(Music_Volume_KEY, sliderValue);
    }

    public void SetSoundEffectsLevel(float sliderValue)
    {
        _masterMixer.SetFloat("SoundEffects", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat (SFX_Volume_KEY, sliderValue);
    }
}

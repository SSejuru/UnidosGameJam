using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_SOUND
{
    Success,
    Failed,
    ManaGain,
    SpellCast,
}

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField]
    private AudioSource _backgroundMusic;
    [SerializeField]
    private AudioSource _playerClick;
    [SerializeField]
    private AudioSource _soundFX;

    [Space(10)]  
    [SerializeField] private List<Sound> _gameSounds = new List<Sound>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _playerClick.Play();
        } 
    }
}

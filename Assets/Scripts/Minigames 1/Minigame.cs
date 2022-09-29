using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour, IInteractable
{
    [Header("Minigame in MinigameCanvas")]
    [SerializeField]
    protected GameObject _canvasMinigame;

    [Header("Minigame Data")]
    [SerializeField]
    protected MinigameType _type;
    [SerializeField]
    protected float _manaOnWin = 20;
    [SerializeField]
    protected float _timeToFinishMinigame = 30f;
    [SerializeField]
    protected float _cooldownTimer = 10f;

    private bool _isOnCooldown = false;
    private float _timerCD = 0f;

    public float TimeToFinishMinigame { get => _timeToFinishMinigame; }

    protected virtual void Update()
    {
        CheckCDTimer();
    }

    public void Interact()
    {
        if(!_isOnCooldown)
            StartMinigame();
    }

    /// <summary>
    /// Starts minigame, and notifies mini games manager to start timer 
    /// </summary>
    public virtual void StartMinigame()
    {
        Debug.Log(_type + " minigame started"); // Debugs for dparty

        _canvasMinigame.SetActive(true);
        ManagerLocator.Instance._miniGamesManager.StartMinigame(this);
    }

    /// <summary>
    /// Checks if player wins and closes minigame, activating its cooldown
    /// </summary>
    public void EndMinigame(bool minigameWon)
    {
        Debug.Log(_type + " minigame ended, Win status : " + minigameWon); // Debugs for dparty

        if (minigameWon)
        {
            ManagerLocator.Instance._miniGamesManager.StopMinigameTimer();
            ManagerLocator.Instance._playerController.AddMana(_manaOnWin);
        }

        _isOnCooldown = true;      
        _canvasMinigame.SetActive(false);

        ManagerLocator.Instance._playerController.SetMovingStatus(true);
        ManagerLocator.Instance._uiManager.EnableSpellBarData(true);
        ManagerLocator.Instance._playerController.InteractionComp.ToggleInteraction(true);


        //Activate UI for cooldown in gameObject (TODO Later)
    }


    /// <summary>
    /// Handles cooldown once player finishes game
    /// </summary>
    private void CheckCDTimer()
    {
        if (_isOnCooldown)
        {
            if (_timerCD <= _cooldownTimer)
                _timerCD += Time.deltaTime;
            else
            {
                _isOnCooldown = false;
                _timerCD = 0f;
            }

        }
    }

}

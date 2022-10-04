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
    protected float _manaOnWin = 20;
    [SerializeField]
    protected float _timeToFinishMinigame = 30f;
    [SerializeField]
    protected float _cooldownTimer = 10f;

    [Header("CircleSprites")]
    [SerializeField] protected Sprite _turnedOffSprite;
    [SerializeField] protected Sprite _turnedOnSprite;

    [Space(20)]

    private bool _isOnCooldown = false;
    private float _timerCD = 0f;

    public float TimeToFinishMinigame { get => _timeToFinishMinigame; }
    public float ManaOnWin { get => _manaOnWin; }
    public GameObject CanvasMinigame { get => _canvasMinigame; }
    public bool IsOnCooldown { get => _isOnCooldown; set => _isOnCooldown = value; }

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
        ManagerLocator.Instance._playerController.ToggleInteractIcon(false);
        _canvasMinigame.SetActive(true);
        ManagerLocator.Instance._miniGamesManager.StartMinigame(this);
    }

    /// <summary>
    /// Checks if player wins and closes minigame, activating its cooldown
    /// </summary>
    public virtual void EndMinigame(bool minigameWon)
    {
        _isOnCooldown = true;    
        GetComponent<SpriteRenderer>().sprite = _turnedOffSprite;

        ManagerLocator.Instance._miniGamesManager.EndMinigameStatus(minigameWon);

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
                CheckIfPlayerIsOnCircle();
                GetComponent<SpriteRenderer>().sprite = _turnedOnSprite;
                _timerCD = 0f;
            }

        }
    }

    private void CheckIfPlayerIsOnCircle()
    {
        LayerMask mask = LayerMask.GetMask("Player");

        Collider2D playerCollider = Physics2D.OverlapCircle(gameObject.transform.position, 2, mask);

        if(playerCollider != null)
        {
            ManagerLocator.Instance._playerController.ToggleInteractIcon(true);
        }
    }

}

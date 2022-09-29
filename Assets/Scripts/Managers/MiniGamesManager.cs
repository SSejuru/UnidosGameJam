using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum MinigameType
{
    RuneWordle,
}

public class MiniGamesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _timer;

    [SerializeField]
    private TextMeshProUGUI _timerText;

    private Minigame _currentActiveMinigame;

    private float _currentTimer = 0;

    private bool _activeTimer = false;

    public void StartMinigame(Minigame game)
    {
        ManagerLocator.Instance._playerController.SetMovingStatus(false);
        ManagerLocator.Instance._uiManager.EnableSpellBarData(false);

        _currentActiveMinigame = game;
        _currentTimer = _currentActiveMinigame.TimeToFinishMinigame; 

        //Set text to timerText and turn on timerGOBJ

        _activeTimer = true;
    }

    public void StopMinigameTimer()
    {
        _activeTimer = false;

        //Turn off  timerText and timerGOBJ
    }

    private void Update()
    {
        if (_activeTimer)
        {
            if(_currentTimer > 0)
                _currentTimer -= Time.deltaTime;
            else
            {
                _activeTimer = false;
                _currentActiveMinigame.EndMinigame(false);
            }

        }
    }

}

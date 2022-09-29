using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [Header("Sucess Panel Data")]
    [SerializeField] private GameObject _successPanel;
    [SerializeField] private Sprite _sucessSprite;
    [SerializeField] private Sprite _failSprite;

    private Minigame _currentActiveMinigame;
    private Minigame _previousMinigame;

    private float _currentTimer = 0;

    private bool _activeTimer = false;

    private Vector2 _startTimerPosition = Vector2.zero;

    private void Start()
    {
        _startTimerPosition = _timer.gameObject.transform.position;
    }

    public void StartMinigame(Minigame game)
    {
        ManagerLocator.Instance._playerController.SetMovingStatus(false);
        ManagerLocator.Instance._uiManager.EnableSpellBarData(false);
        ManagerLocator.Instance._playerController.InteractionComp.ToggleInteraction(false);

        AnimatePanelIn(game.CanvasMinigame.transform);

        _currentActiveMinigame = game;
        _currentTimer = _currentActiveMinigame.TimeToFinishMinigame; 

        //Set text to timerText and turn on timerGOBJ
        _timer.gameObject.SetActive(true);
        _timer.transform.localScale = Vector2.zero;
        _timer.transform.LeanScale(Vector2.one, 0.5f);

        _timerText.text = _currentTimer.ToString("F0");

        _activeTimer = true;
    }


    private void AnimatePanelIn(Transform panelTransform)
    {
        panelTransform.localPosition = new Vector2(0, -Screen.height);
        panelTransform.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;
    }

    private void AnimatePanelOut(Transform panelTransform)
    {
        panelTransform.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo().setOnComplete(TurnOffPanel);
    }

    private void TurnOffPanel()
    {
        _currentActiveMinigame.CanvasMinigame.gameObject.SetActive(false);

        ManagerLocator.Instance._playerController.SetMovingStatus(true);
        ManagerLocator.Instance._uiManager.EnableSpellBarData(true);
        ManagerLocator.Instance._playerController.InteractionComp.ToggleInteraction(true);
        ManagerLocator.Instance._playerController.AddMana(_previousMinigame.ManaOnWin);
    }

    public void EndMinigameStatus(bool status)
    {
        _previousMinigame = _currentActiveMinigame;
        StartCoroutine(ShowFinalPanel(status));
        _activeTimer = false;
        _timer.transform.LeanScale(Vector2.zero, 0.5f).setEaseInBack();
    }

    private void Update()
    {
        if (_activeTimer)
        {
            if (_currentTimer > 0)
            {
                _currentTimer -= Time.deltaTime;
                _timerText.text = _currentTimer.ToString("F0");
            }
            else
            {
                _currentActiveMinigame.EndMinigame(false);
            }

        }
    }

    private IEnumerator ShowFinalPanel(bool value)
    {
        _successPanel.SetActive(true);

        if (value)
            _successPanel.GetComponent<Image>().sprite = _sucessSprite;
        else
            _successPanel.GetComponent<Image>().sprite = _failSprite;

        _successPanel.transform.localScale = Vector2.zero;
        _successPanel.transform.LeanScale(Vector2.one, 0.5f);

        yield return new WaitForSeconds(1.5f);

        _successPanel.transform.LeanScale(Vector2.zero, 0.5f).setEaseInBack();

        yield return new WaitForSeconds(1f);

        AnimatePanelOut(_currentActiveMinigame.CanvasMinigame.transform);


        yield return null;
    }

}

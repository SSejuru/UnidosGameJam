using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RuneSimonMinigame : Minigame
{
    [SerializeField]
    private GameObject _getReadyText;
    [SerializeField]
    private Image _messageImage;

    [SerializeField]
    private List<RuneButton> _buttons;

    [SerializeField]
    private List<Rune> _runes = new List<Rune>();

    private Rune[] _answer = new Rune[4];

    private bool _canClick = true;
    private int _answerCont = 0;

    private const string GET_READY_TEXT = "PAY ATTENTION To the secuencia divina";
    private const string USE_SEQUENCE = "Enter the secuencia divina";

    public override void StartMinigame()
    {
        _answerCont = 0;
        _messageImage.enabled = false;
        _canClick = true;
        SetAnswer();
        base.StartMinigame();
    }

    public override void EndMinigame(bool minigameWon)
    {
        _canClick = false;
        base.EndMinigame(minigameWon);
    }

    private void SetAnswer()
    {
        for (int i = 0; i < _answer.Length; i++)
        {
            _answer[i] = _runes[Random.Range(0, _answer.Length)];
        }

        SetButtonInteractions(false);
        StartCoroutine(StartShowingSequence());       
    }

    private IEnumerator StartShowingSequence()
    {
        _getReadyText.SetActive(true);
        _getReadyText.GetComponent<TextMeshProUGUI>().text = GET_READY_TEXT;
        ManagerLocator.Instance._soundManager.PlaySoundEffect(ENUM_SOUND.ShowRuneSymbol);

        yield return new WaitForSeconds(2);
        _getReadyText.SetActive(false);
        ManagerLocator.Instance._soundManager.PlaySoundEffect(ENUM_SOUND.HideRuneSymbol);

        yield return new WaitForSeconds(1f);


        for (int i = 0; i < _answer.Length; i++)
        {
            _messageImage.enabled = true;
            _messageImage.sprite = _answer[i].runeIcon;
            ManagerLocator.Instance._soundManager.PlaySoundEffect(ENUM_SOUND.ShowRuneSymbol);
            yield return new WaitForSeconds(1f);
            _messageImage.enabled = false;
            ManagerLocator.Instance._soundManager.PlaySoundEffect(ENUM_SOUND.HideRuneSymbol);

            yield return new WaitForSeconds(1f);
        }
        _getReadyText.SetActive(true);
        _getReadyText.GetComponent<TextMeshProUGUI>().text = USE_SEQUENCE;
        SetButtonInteractions(true);
        ManagerLocator.Instance._soundManager.PlaySoundEffect(ENUM_SOUND.ShowRuneSymbol);



        yield return null;
    }

    public void CheckAnswer(RuneButton btn)
    {
        if (_canClick)
        {
            if(btn.type == _answer[_answerCont].type)
            {
                _answerCont++;
                if(_answerCont == _answer.Length)
                {
                    EndMinigame(true);
                }
            }
            else
            {
                EndMinigame(false);
            }
        }
    }

    private void SetButtonInteractions(bool value)
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].GetComponent<Button>().interactable = value;
        }
    }

}

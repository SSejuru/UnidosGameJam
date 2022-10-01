using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Runes
{
    RuneHalcon = 0,
    RuneCulebra = 1,
    RunePerdiz = 2,
    RuneZamut = 3,
    RuneRepelon = 4
}

public class RuneWordle : Minigame
{
    [Header("Rune scriptables")]
    [SerializeField] Rune[] _runesScriptables = new Rune[5];

    [Space(10)]

    [SerializeField]
    private Sprite _emptySprite;
    [SerializeField]
    private Button _button;
    [SerializeField]
    private RunePanel _guessPanel;
    [SerializeField]
    private RunePanel[] _triesPanel = new RunePanel[4];


    int _randNumber;
    int _count = 0;
    int _tryAttempt = 0;
    int[] _rightAnswerPositions = new int[4];
    bool _rightAnswer = false;
    bool _rightWord = false;
    bool _containsWord = false;
    Runes _rune;
    Runes[] _runeRandomSequence = new Runes[4];
    Runes[] _runeGuessSequence = new Runes[4];
    Sprite[] _runeGuessSequenceImages = new Sprite[4];



    public override void StartMinigame()
    {
        RandomRuneSequence();
        _button.interactable = false;
        base.StartMinigame();
    }



    private void RandomRuneSequence()
    {

        for (int i = 0; i < 4; i++)
        {
            _randNumber = Random.Range(0, 5);
            _rune = (Runes)_randNumber;
            _runeRandomSequence[i] = _rune;
            Debug.Log(_runeRandomSequence[i]);
        }
    }

    public void RuneBtn(RuneButton button)
    {
        Runes runePress = button.type;
        GuessRunes(runePress);

    }

    private void GuessRunes(Runes runePress)
    {

        if (_count < 4)
        {
            _runeGuessSequence[_count] = runePress;
            RuneSpriteChangerUI(GetRuneSprite(runePress));
            _runeGuessSequenceImages[_count] = GetRuneSprite(runePress);
            _count++;
        }
        SubmitButtonCheck();
    }

    public void SubmitAnswer()
    {
        //Debug.Log("respuesta enviada");
        ResetCount();
        RuneSequenceCorrection();
        RuneTriesPanelChanger(_runeGuessSequenceImages);
        AnswerSequence();
        RuneSpriteChangerReset();
        SubmitButtonCheck();
    }

    private void AnswerSequence()
    {

        if (!_rightAnswer)
        {

            if (_rightAnswerPositions[0] == 1 && _rightAnswerPositions[1] == 1 && _rightAnswerPositions[2] == 1 && _rightAnswerPositions[3] == 1)
            {
                _rightWord = true;
            }
            else
            {
                _rightWord = false;
            }


            if (!_rightWord)
            {
                _rightAnswer = false;
                _tryAttempt++;
            }

            if (_rightWord)
            {
                _rightAnswer = true;
            }

        }

        if (_rightAnswer)
        {
            EndMinigame(true);
            _rightAnswer = false;
            _tryAttempt = 0;
            ResetTriesPanel();
        }
    }

    private void RuneSequenceCorrection()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (_runeRandomSequence[i] == _runeGuessSequence[j])
                {
                    _containsWord = true;
                    break;
                }
                else
                {
                    _containsWord = false;
                }
            }

            if (_runeRandomSequence[i] == _runeGuessSequence[i])
            {
                _rightAnswerPositions[i] = 1;
            }
            else if (_runeRandomSequence[i] != _runeGuessSequence[i] && _containsWord)
            {
                _rightAnswerPositions[i] = 2;
                _containsWord = false;
            }
            else
            {
                _rightAnswerPositions[i] = 0;
            }
        }
    }

    private void RuneSpriteChangerUI(Sprite runeSprite)
    {
        if (_count < 4)
        {
            _guessPanel.RuneImages[_count].sprite = runeSprite;
        }
    }

    private void RuneTriesPanelChanger(Sprite[] _trySprites)
    {
        if (_tryAttempt < 4)
        {
            for (int i = 0; i < 4; i++)
            {
                _triesPanel[_tryAttempt].RuneImages[i].sprite = _trySprites[i];


                if (_rightAnswerPositions[i] == 0)
                {
                    _triesPanel[_tryAttempt].RuneImages[i].color = Color.red;
                }
                else if (_rightAnswerPositions[i] == 1)
                {
                    _triesPanel[_tryAttempt].RuneImages[i].color = Color.green;
                }
                else if (_rightAnswerPositions[i] == 2)
                {
                    _triesPanel[_tryAttempt].RuneImages[i].color = Color.yellow;
                }
            }
        }


    }

    private void RuneSpriteChangerReset()
    {
        for (int i = 0; i < 4; i++)
        {
            _guessPanel.RuneImages[i].sprite = _emptySprite;
        }
    }

    private void SubmitButtonCheck()
    {
        if (_count == 4)
        {
            _button.interactable = true;
        }
        else
        {
            _button.interactable = false;
        }

        if (_tryAttempt == 4)
        {
            EndMinigame(false);
            _rightAnswer = false;
            _tryAttempt = 0;
            ResetTriesPanel();
        }
    }

    // Reset the submit button and count variable
    private void ResetCount()
    {
        _button.interactable = false;
        _count = 0;

    }

    private void ResetTriesPanel()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                _triesPanel[i].RuneImages[j].sprite = _emptySprite;
                _triesPanel[i].RuneImages[j].color = Color.white;
            }

        }
    }

    private Sprite GetRuneSprite(Runes runeType)
    {
        for (int i = 0; i < _runesScriptables.Length; i++)
        {
            if (_runesScriptables[i].type == runeType)
            {
                return _runesScriptables[i].img;
            }
        }

        return null;
    }
}

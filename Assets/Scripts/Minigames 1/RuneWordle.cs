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

    [SerializeField]
    private Button _button;
    [SerializeField]
    private RunePanel _guessPanel;
    [SerializeField]
    private RunePanel[] _triesPanel = new RunePanel[4];


    int _randNumber;
    int _count = 0;
    int[] _rightAnswerPositions = new int[4];
    bool _rightAnswer = false;
    bool _rightWord = false;
    bool _containsWord = false;
    Runes _rune;
    Runes[] _runeRandomSequence = new Runes[4];
    Runes[] _runeGuessSequence = new Runes[4];



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
            _count++;
        }
        SubmitButtonCheck();
    }

    private void SubmitAnswer()
    {
        //Debug.Log("respuesta enviada");
        ResetCount();
        RuneSequenceCorrection();
        AnswerSequence();
    }

    private void AnswerSequence()
    {
        
        if (!_rightAnswer)
        {
            for (int i = 0; i < 4; i++)
            {
                if (_rightAnswerPositions[i] == 1)
                {
                    _rightWord = true;
                }
                else
                {
                    _rightWord = false;
                }
            }

            if(!_rightWord )
            {
                _rightAnswer = false;
                Debug.Log("Aun nooooooooo");
            }
            if (_rightWord)
            {
                _rightAnswer = true;
            }
            
        }

        if(_rightAnswer)
        {
            Debug.Log("Ganaste");
            _rightAnswer = false;
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
                }
                else
                {
                    _containsWord = false;
                }
            }

            if (_runeRandomSequence[i] == _runeGuessSequence[i] && _containsWord)
            {
                _rightAnswerPositions[i] = 1;
            }
            else if (_runeRandomSequence[i] != _runeGuessSequence[i] && _containsWord)
            {
                _rightAnswerPositions[i] = 2;
            }
            else
            {
                _rightAnswerPositions[i] = 0;
            }
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
    }

    // Reset the submit button and count variable
    private void ResetCount()
    {
        _button.interactable = false;
        _count = 0;
    }
}

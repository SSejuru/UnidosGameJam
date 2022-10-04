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
    bool _containsWord = false;
    Runes _rune;
    Runes[] _runeRandomSequence = new Runes[4];
    Runes[] _runeGuessSequence = new Runes[4];
    Sprite[] _runeGuessSequenceImages = new Sprite[4];



    public override void StartMinigame()
    {
        _tryAttempt = 0;
        RandomRuneSequence();
        ResetTriesPanel();
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
        RuneSequenceCorrection(_runeGuessSequenceImages);
        ResetCount();
        RuneSpriteChangerReset();
        AnswerSequence();
        SubmitButtonCheck();
    }

    //Analize the game and finish it if the conditions are met if not add a try attempt
    private void AnswerSequence()
    {
       
        if (_triesPanel[_tryAttempt].RuneImages[0].color == Color.green && _triesPanel[_tryAttempt].RuneImages[1].color == Color.green && _triesPanel[_tryAttempt].RuneImages[2].color == Color.green && _triesPanel[_tryAttempt].RuneImages[3].color == Color.green)
        {
            EndMinigame(true);
        }
        else if (_tryAttempt == 4)
        {

            EndMinigame(false);
        }
        else
        {
            _tryAttempt++;
        }

    }

    private void RuneSequenceCorrection(Sprite[] _trySprites)
    {
        for (int i = 0; i < _runeRandomSequence.Length; i++)
        {
            _triesPanel[_tryAttempt].RuneImages[i].sprite = _trySprites[i];

            if (_runeRandomSequence[i] == _runeGuessSequence[i])
            {
                
                _triesPanel[_tryAttempt].RuneImages[i].color = Color.green;
            }

            else if (_runeRandomSequence[i] != _runeGuessSequence[i])
            {

                for (int j = 0; j < 4; j++)
                {
                    if (_runeRandomSequence[j] == _runeGuessSequence[i])
                    {
                        if (_runeRandomSequence[j] == _runeGuessSequence[j])
                        {
                            
                            _triesPanel[_tryAttempt].RuneImages[i].color = Color.red;
                        }
                        else
                        {
                            _containsWord = true;
                        }

                    }

                }
                if (_containsWord)
                {
                   
                    _triesPanel[_tryAttempt].RuneImages[i].color = Color.yellow;
                    _containsWord = false;
                }

                else
                {
                   
                    _triesPanel[_tryAttempt].RuneImages[i].color = Color.red;
                }
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

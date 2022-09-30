using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunePatternMinigame : Minigame
{
    [Header("Game Data")]
    [SerializeField] private RunePanel _mainPanel;
    [SerializeField] private RunePanel _answerPanel;
    [SerializeField] private List<Rune> _runeList = new List<Rune>();

    private Rune[] _answer = new Rune[4];
    private int _runeCont = 0;
    private bool _canClick = true;

    public override void StartMinigame()
    {
        _runeCont = 0;
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
            _answer[i] = _runeList[Random.Range(0, _runeList.Count)];
            _mainPanel.RuneImages[i].sprite = _answer[i].img;
            _mainPanel.RuneImages[i].color = Color.black;
            _answerPanel.RuneImages[i].sprite = _answer[i].runeIcon;
        }
    }

    public void ClickedOption(RuneButton btn)
    {
        if(_canClick)
        {
            if(btn.type == _answer[_runeCont].type)
            {
                _mainPanel.RuneImages[_runeCont].color = Color.white;
                _runeCont++;

                if(_runeCont == 4)
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
}

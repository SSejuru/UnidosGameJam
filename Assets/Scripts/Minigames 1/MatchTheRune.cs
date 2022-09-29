using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTheRune : Minigame
{
    [SerializeField] MTRCell[] _gridCells = new MTRCell[10];
    [SerializeField] Rune[] _runes = new Rune[5];

    public override void StartMinigame()
    {
        SetUpRunes();
        base.StartMinigame();
    }

    private void SetUpRunes()
    {
        List<int> occupiedCells = new List<int>();

        for (int p = 0; p < 2; p++)
        {
            for (int i = 0; i < _runes.Length; i++)
            {
                int random = Random.Range(0, 10);

                if (!occupiedCells.Contains(random))
                {
                    occupiedCells.Add(random);
                    _gridCells[random].RuneType = _runes[i].type;
                    _gridCells[random].RuneImage.sprite = _runes[i].img;
                }
                else
                {
                    i--;
                }
            }
        }
    }



}

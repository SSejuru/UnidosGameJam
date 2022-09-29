using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTheRune : Minigame
{
    [SerializeField] MTRCell[] _gridCells = new MTRCell[10];
    [SerializeField] Rune[] _runes = new Rune[5];

    private List<MTRCell> _activeRunes = new List<MTRCell>();
    int _matchesCompleted = 0;

    private void Start()
    {
        for(int i = 0; i < _gridCells.Length; i++)
        {
            _gridCells[i].Initialize(this);
        }
    }

    public override void StartMinigame()
    {
        for (int i = 0; i < _gridCells.Length; i++)
        {
            _gridCells[i].RuneImage.gameObject.SetActive(false);
        }

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

    public override void EndMinigame(bool minigameWon)
    {
        if(!minigameWon)
            StopAllCoroutines();

        _matchesCompleted = 0;
        _activeRunes.Clear();

        base.EndMinigame(minigameWon);
    }

    public void ActivateRune(MTRCell cell)
    {
        if(_activeRunes.Count < 2)
        {
            _activeRunes.Add(cell);
            cell.RuneImage.gameObject.SetActive(true);
            if (_activeRunes.Count == 2)
                StartCoroutine(HideActiveRunes());
        }
    }

    private IEnumerator HideActiveRunes()
    {
        yield return new WaitForEndOfFrame();
        if(_activeRunes[0].RuneType == _activeRunes[1].RuneType)
        {
            _activeRunes.Clear();
            _matchesCompleted++;
            
            if(_matchesCompleted == 5)
            {
                EndMinigame(true);
            }

            yield break;
        }
        else
        {
            MTRCell cell1 = _activeRunes[0];
            MTRCell cell2 = _activeRunes[1];

            yield return new WaitForSeconds(0.5f);

            _activeRunes.Clear();

            yield return new WaitForSeconds(0.5f);

            cell1.RuneImage.gameObject.SetActive(false);
            cell2.RuneImage.gameObject.SetActive(false);
        }

        yield return null;
    }
}

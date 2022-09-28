using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    int[] _runeSequence = new int[4];
    int _randNumber;
    
    public override void StartMinigame()
    {
        base.StartMinigame();
        RandomRuneSequence();
    }

    private void RandomRuneSequence()
    {
        _randNumber = Random.Range(0, 5);

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rune", menuName = "Rune")]
public class Rune : ScriptableObject
{
    public Runes type;
    public Sprite img;
    public Sprite runeIcon;
}

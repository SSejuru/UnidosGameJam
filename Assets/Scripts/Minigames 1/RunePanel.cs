using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunePanel : MonoBehaviour
{
    [SerializeField]
    Image[] _runeImages = new Image[4];

    public Image[] RuneImages { get => _runeImages; set => _runeImages = value; }
}

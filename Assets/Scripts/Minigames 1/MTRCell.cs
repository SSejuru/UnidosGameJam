using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MTRCell : SimpleButton
{
    [SerializeField] private Image _runeImage;
    private Runes _runeType;

    public Image RuneImage { get => _runeImage; set => _runeImage = value; }
    public Runes RuneType { get => _runeType; set => _runeType = value; }

    public override void OnPointerClick(PointerEventData eventData)
    {
        _runeImage.gameObject.SetActive(true);
    }
}

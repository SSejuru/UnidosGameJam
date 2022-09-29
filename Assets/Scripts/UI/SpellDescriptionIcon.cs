using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellDescriptionIcon : SimpleButton
{
    public Spell _spell;
    public Image _spellIcon;

    private void Start()
    {
        _spellIcon.sprite = _spell.spellImage;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        ManagerLocator.Instance._uiManager.HoverSpellBookIcon(_spell);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        ManagerLocator.Instance._uiManager.HideSpellBookDescription();
    }
}

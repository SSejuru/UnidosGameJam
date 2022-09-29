using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellDescriptionIconChildOverride : SimpleButton
{
    public override void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInParent<SpellDescriptionIcon>().OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInParent<SpellDescriptionIcon>().OnPointerExit(eventData);
    }
}

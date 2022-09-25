using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    [SerializeField] private List<SpellUI> _uiSpells = new List<SpellUI>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSpell(SpellUI spell)
    {
        _uiSpells.Add(spell);
    }

    // Update players and NPC health, mana

    public void UIManaUpdate(float instigatorMana, LivingBeing instigator)
    {
        // Update spells and UI
    }

    public void UIHealthUpdate(float instigatorHealth, LivingBeing instigator)
    {
        //Update Health in UI
    }

    public void UIShieldUpdate(float currentShield, LivingBeing instigator)
    {
        //Update shield in UI
    }
}

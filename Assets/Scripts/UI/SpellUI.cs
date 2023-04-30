using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpellUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private Spell _magicSpell;

    [SerializeField]
    private Image _spellIcon;
    [SerializeField]
    private Image _spellInactiveIcon;

    private bool _isOnCoolDown = false;
    private bool _canBeCasted = false;

    private float _currentPlayerMana = 0f;

    // Start is called before the first frame update
    void Start()
    {
        ManagerLocator.Instance._uiManager.AddSpell(this);

        // Set up Data
        _spellIcon.sprite = _magicSpell.spellImage;
        _spellInactiveIcon.sprite = _magicSpell.spellImage;
        _currentPlayerMana = ManagerLocator.Instance._playerController.CurrentMana;
        CheckForSpellStatus();
    }

    public void UpdateStatus(float playerMana)
    {
        _currentPlayerMana = playerMana;
        CheckForSpellStatus();
    }

    private void CheckForSpellStatus()
    {
        if (!_isOnCoolDown && _currentPlayerMana >= _magicSpell.spellCost)
        {
            _spellInactiveIcon.enabled = false;
            _canBeCasted = true;
        }
        else
        {
            _spellInactiveIcon.enabled = true;
            _canBeCasted = false;
        }
    }

    public void TryCast()
    {
        if (_canBeCasted)
        {
            if (_magicSpell.spellTarget == SpellTarget.MouseTarget)
            {
                // Find target with click
            }
            else
            {
                ManagerLocator.Instance._spellManager.CastSpell(_magicSpell);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TryCast();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Player Canvas")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private CanvasGroup _playerManaCanvas;

    [Header("Player Data")]
    [SerializeField] private TextMeshProUGUI _playerManaValue;
    [SerializeField] private TextMeshProUGUI _playerManaPerSecond;

    [Header("Spell Data")]
    [SerializeField] private GameObject _spellContainer;
    [SerializeField] private GameObject _spellBookButton;

    [Header("Spellbook Data")]
    [SerializeField] private GameObject _spellBookPanel;
    [SerializeField] private TextMeshProUGUI _spellTitle;
    [SerializeField] private TextMeshProUGUI _spellDescription;
    [SerializeField] private TextMeshProUGUI _spellCost;
    [SerializeField] private GameObject _manaIconImage;

    private List<SpellUI> _uiSpells = new List<SpellUI>();

    private bool _isDescriptionPanelActive = false;

    private void Start()
    {
        HideSpellBookDescription();
    }

    public void ToggleSpellBookDescriptionPanel()
    {
        _isDescriptionPanelActive = !_isDescriptionPanelActive;

        _spellBookPanel.SetActive(_isDescriptionPanelActive);
        _spellContainer.SetActive(!_isDescriptionPanelActive);
        ManagerLocator.Instance._playerController.InteractionComp.ToggleInteraction(!_isDescriptionPanelActive);
    }

    public void HoverSpellBookIcon(Spell spell)
    {
        _manaIconImage.SetActive(true);
        _spellCost.text = spell.spellCost.ToString();
        _spellDescription.text = spell.description;
        _spellTitle.text = spell.spellName;
    }

    public void HideSpellBookDescription()
    {
        _manaIconImage.SetActive(false);
        _spellCost.text = "";
        _spellDescription.text = "";
        _spellTitle.text = "";
    }


    public void AddSpell(SpellUI spell)
    {
        _uiSpells.Add(spell);
    }

    // Update players and NPC health, mana

    public void DisplayManaAnimation(float AddedMana)
    {
        StartCoroutine(ShowManaAnimation(AddedMana));
    }

    private IEnumerator ShowManaAnimation(float AddedMana)
    {
        _playerManaCanvas.gameObject.SetActive(true);
        _playerManaCanvas.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "+" + AddedMana.ToString("F0");
        yield return new WaitForEndOfFrame();
        _playerManaCanvas.alpha = 0;
        _playerManaCanvas.LeanAlpha(1, 0.5f);

        yield return new WaitForSeconds(1.5f);

        _playerManaCanvas.LeanAlpha(0, 0.5f);

        yield return new WaitForSeconds(0.7f);

        _playerManaCanvas.gameObject.SetActive(false);

        yield return null;
    }

    public void UIManaUpdate(float playerMana)
    {
        // Update spells and UI
        for (int i = 0; i < _uiSpells.Count; i++)
        {
            _uiSpells[i].UpdateStatus(playerMana);
        }

        _playerManaValue.text = playerMana.ToString("F0");     
    }

    public void ManaPerSecondUIUpdate(float mps)
    {
        _playerManaPerSecond.text = "MPS: " + mps.ToString("F1", System.Globalization.CultureInfo.InvariantCulture);
    }

    public void EnableSpellBarData(bool value)
    {
        _spellContainer.gameObject.SetActive(value);
        _spellBookButton.gameObject.SetActive(value);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Game Panels")]
    [SerializeField]
    private GameObject _playerUIContainer;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _mainMenuContainer;

    [Space(10)]

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
    private bool _isSettingsPanelActive = false;
    private bool _isAnimating = false;

    private void Start()
    {
        HideSpellBookDescription();
    }

    public void StartGame()
    {
        _playerUIContainer.SetActive(true);
    }


    public void ToggleSettingsPanel()
    {
        if (_isAnimating)
            return;

        _isSettingsPanelActive = !_isSettingsPanelActive;

        if (_isSettingsPanelActive)
        {
            _settingsPanel.SetActive(true);
            ShowPanelSlide(_settingsPanel);
        }
        else
        {
            HidePanelSlide(_settingsPanel);
            StartCoroutine(DeactivateObjectAfterSeconds(_settingsPanel, 0.5f));
        }
    }

    private IEnumerator DeactivateObjectAfterSeconds(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);   
        obj.SetActive(false);
    }

    public void ShowPanelSlide(GameObject obj)
    {
        _isAnimating = true;
        Transform panelTransform = obj.transform;
        panelTransform.localPosition = new Vector2(0, -Screen.height);
        panelTransform.LeanMoveLocalY(0, 0.5f).setOnComplete(StopAnimating).setEaseOutExpo().delay = 0.1f;
    }

    public void HidePanelSlide(GameObject obj)
    {
        _isAnimating = true;
        Transform panelTransform = obj.transform;
        panelTransform.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo().setOnComplete(StopAnimating);
    }

    private void StopAnimating()
    {
        _isAnimating = false;
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
        string addedText = AddedMana >= 0 ? "+" + AddedMana.ToString("F0") : AddedMana.ToString("F0");
        _playerManaCanvas.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = addedText;
        yield return new WaitForEndOfFrame();
        _playerManaCanvas.alpha = 0;
        _playerManaCanvas.LeanAlpha(1, 0.2f);
        ManagerLocator.Instance._soundManager.PlaySoundEffect(ENUM_SOUND.ManaGain);
        yield return new WaitForSeconds(1f);

        _playerManaCanvas.LeanAlpha(0, 0.2f);

        yield return new WaitForSeconds(0.5f);

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

}

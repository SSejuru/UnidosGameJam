using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class StoryManager : MonoBehaviour
{

    private Queue<string> _dialogueSentences;

    [Header("Dialogue System")]
    [SerializeField] private GameObject _dialogueContainer;
    [SerializeField] private GameObject _continueButton;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private TextMeshProUGUI _dialogueName;
    [SerializeField] private Image _dialogueImage;

    [Header("Dialogues")]
    [SerializeField] private DialogueStory _firstDialogue;
    [SerializeField] private DialogueStory _playerArrivingDialogue;
    [SerializeField] private DialogueStory _magicExplanationDialogue;
    [SerializeField] private DialogueStory _finalDialogue;

    [Header("UI Objects")]
    [SerializeField] private List<Button> _menuButtons = new List<Button>();
    [SerializeField] private CanvasGroup _menuCanvas;
    [SerializeField] private CanvasGroup _fadeToBlackPanel;
    [SerializeField] private Button _skipButton;

    [Header("Virtual Cameras")]
    [SerializeField] private CinemachineBrain _brain;
    [SerializeField] private CinemachineVirtualCamera _menuVC;
    [SerializeField] private CinemachineVirtualCamera _mainCamera;
    [SerializeField] private CinemachineVirtualCamera _npcCamera;
    [SerializeField] private CinemachineVirtualCamera _spellStationCamera;

    private bool _isStoryPlaying = false;
    private int _dialogueCont = 0;
    private bool _isDialoguePlaying = false;
    private bool _isSkipping = false;
    private PlayerController _playerController;

    private IEnumerator _sentenceDrawingCoroutine;

    private Vector2 _playerStartPosition = new Vector2(0.03f, 6.58f);


    private void Start()
    {
        _dialogueSentences = new Queue<string>();
        _playerController = ManagerLocator.Instance._playerController;
    }

    public void StartGame()
    {
        ManagerLocator.Instance._npcManager.ToggleAllNPCSHealthBar(false);

        foreach(Button button in _menuButtons)
        {
            button.interactable = false;
        }

        _menuCanvas.LeanAlpha(0, 0.5f).setOnComplete(DisableMenu);
    }

    private void DisableMenu()
    {
        _menuCanvas.gameObject.SetActive(false);
        _menuVC.gameObject.SetActive(false);
        StartCoroutine(StartDialogues());
    }

    IEnumerator StartDialogues()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(IsCinemachineNotBlending);
        _skipButton.gameObject.SetActive(true);
        StartCoroutine(StartStory(_firstDialogue));
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(IsStoryOver);
        _playerController.gameObject.SetActive(true);
        _playerController.CanMove = false;
        _playerController.GetComponent<Rigidbody2D>().velocity = Vector2.up * 2.8f;
        yield return new WaitForSeconds(2.5f);
        _playerController.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        // Wait until player arrives
        StartCoroutine(StartStory(_playerArrivingDialogue));
        yield return new WaitUntil(IsStoryOver);
        _npcCamera.gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(IsCinemachineNotBlending);
        StartCoroutine(StartStory(_magicExplanationDialogue));
        yield return new WaitUntil(IsStoryOver);
        _npcCamera.gameObject.SetActive(true);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(IsCinemachineNotBlending);
        StartCoroutine(StartStory(_finalDialogue));
        yield return new WaitUntil(IsStoryOver);
        yield return new WaitForEndOfFrame();
        StartCoroutine(InitializeGame());
    }

    IEnumerator InitializeGame()
    {
        _skipButton.gameObject.SetActive(false);
        _fadeToBlackPanel.LeanAlpha(1, 1);
        yield return new WaitForSeconds(1);
        ManagerLocator.Instance._npcManager.ToggleAllNPCSHealthBar(true);
        ManagerLocator.Instance._npcManager.TurnOnAllNPCSSpriteRenderer();
        _playerController.gameObject.SetActive(true);
        yield return new WaitForEndOfFrame();
        ManagerLocator.Instance._uiManager.StartGame();
        _dialogueContainer.SetActive(false);
        _playerController.gameObject.transform.position = _playerStartPosition;
        _npcCamera.gameObject.SetActive(false);
        _spellStationCamera.gameObject.SetActive(false);
        _playerController.CanMove = true;
        _playerController.Initialize();
        _isSkipping = false;
        ManagerLocator.Instance._winConditionManager.InitializeManager();
        ManagerLocator.Instance._enemiesManager.InitializeAllSpawners();
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(IsCinemachineNotBlending);
        _fadeToBlackPanel.LeanAlpha(0, 0.5f);
    }

    public void SkipIntro()
    {
        if (!_isSkipping)
        {
            _isSkipping = true;
            StopAllCoroutines();
            StartCoroutine(InitializeGame());
        }
    }

    IEnumerator StartStory(DialogueStory story)
    {
        _isStoryPlaying = true;

        _dialogueContainer.gameObject.SetActive(true);


        for (int i = 0; i < story._dialogue.Count; i++)
        {
            StartDialogue(story._dialogue[_dialogueCont]);
            yield return new WaitUntil(IsDialogueOver);
        }

        _dialogueCont = 0;
        _dialogueContainer.gameObject.SetActive(false);

        _isStoryPlaying = false;
    }

    private void StartDialogue(Dialogue dialogue)
    {
        _isDialoguePlaying = true;
        _dialogueSentences.Clear();

        _dialogueName.text = dialogue.character.name;
        _dialogueImage.sprite = dialogue.character.sprite;


        foreach (string sentence in dialogue.sentences)
        {
            _dialogueSentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(_sentenceDrawingCoroutine != null)
            StopCoroutine(_sentenceDrawingCoroutine);

        if(_dialogueSentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = _dialogueSentences.Dequeue();
        _sentenceDrawingCoroutine = AnimateSentence(sentence);
        StartCoroutine(_sentenceDrawingCoroutine);
    }

    IEnumerator AnimateSentence(string sentence)
    {
        _dialogueText.text = "";

        foreach(char letter in sentence)
        {
            _dialogueText.text += letter;
            ManagerLocator.Instance._soundManager.PlaySoundEffect(ENUM_SOUND.DialogueLetter);
            yield return new WaitForSeconds(0.04f);
        }

    }

    private void EndDialogue()
    {
        _isDialoguePlaying = false;
        _dialogueCont++;
    }

    private bool IsDialogueOver()
    {
        return !_isDialoguePlaying;
    }

    private bool IsCinemachineNotBlending()
    {
        return !_brain.IsBlending;
    }

    private bool IsStoryOver()
    {
        return !_isStoryPlaying;
    }

}

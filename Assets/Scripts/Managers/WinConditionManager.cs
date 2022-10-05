using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinConditionManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private CanvasGroup _fadeToBlackPanel;
    [SerializeField] private GameObject _panelVictory;
    [SerializeField] private Sprite _panelVictorySprite;
    [SerializeField] private Sprite _panelDefeatSprite;
    [SerializeField] private GameObject _buttonBackToMenu;


    [Header("Data")]
    [SerializeField] public float _startgameTime;

    private float aliveNPC = 0;
    private bool _didPlayerWin = false;
    private float _timer;
    private bool _runningTimer = false;

    public void InitializeManager()
    {
        // Ver cuantos npcs hay vivos al inicio y asignarlos a la variable aliveNPC
        aliveNPC = ManagerLocator.Instance._npcManager.ActiveNPCs.Count;
        // Setear variables del timer --> variable cuanto va a durar la partida la debemos poder editar desde el inspector
        _timer = _startgameTime * 60;
        _runningTimer = true;
    }

    public void ListenerNPCDeath()
    {
        //NPC ha muerto, entonces hay que disminuir variable de npcs vivos
        aliveNPC--;
        // Verificar si estan todos muertos

        if(aliveNPC == 0)
        {
            _didPlayerWin = false;
            FadeToBlack();
        }
        // Si estan todos muertos hacer un fade to black en la pantalla y mostrar una pantalla de derrota
    }

    private void Update()
    {
        if (_runningTimer)
        {
            //Hacer timer aca
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                if (aliveNPC > 0)
                {
                    _didPlayerWin = true;
                }
                else
                {
                    _didPlayerWin = false;
                }

                FadeToBlack();
            }
        }
        // Si el timer se acaba y aliveNpC > 0 gana la partida, hacer un fade to black en la pantalla y mostrar una pantalla de win
    }


    private void FadeToBlack()
    {
        _runningTimer = false;
        _fadeToBlackPanel.LeanAlpha(1, 1f).setOnComplete(ShowFinalScreenPanel);
    }

    private void ShowFinalScreenPanel()
    {
        //Mostrar panel de victoria o derrota segun un booleano (didplayerwin)
        if (_didPlayerWin)
        {
            _panelVictory.GetComponent<Image>().sprite = _panelVictorySprite;
        }else
        {
            _panelVictory.GetComponent<Image>().sprite= _panelDefeatSprite;
        }

        _panelVictory.SetActive(true);
        ManagerLocator.Instance._uiManager.ShowPanelSlide(_panelVictory);
        Invoke("ActivateButton", 1);
    }

    private void ActivateButton()
    {
        _buttonBackToMenu.SetActive(true);
    }

    public void PlayAgain()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinConditionManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup _fadeToBlackPanel;
    [SerializeField] public float _StartgameTime;

    private float aliveNPC = 0;
    private bool _didPlayerWin = false;
    private float _timer;

    public void InitializeManager()
    {
        // Ver cuantos npcs hay vivos al inicio y asignarlos a la variable aliveNPC
        aliveNPC = ManagerLocator.Instance._npcManager.ActiveNPCs.Count;
        // Setear variables del timer --> variable cuanto va a durar la partida la debemos poder editar desde el inspector
        _StartgameTime = _timer;
    }

    public void ListenerNPCDeath()
    {
        //NPC ha muerto, entonces hay que disminuir variable de npcs vivos
        aliveNPC--;
        // Verificar si estan todos muertos

        if(aliveNPC == 0)
        {
            _didPlayerWin = false;

        }
        // Si estan todos muertos hacer un fade to black en la pantalla y mostrar una pantalla de derrota
    }

    private void Update()
    {
        //Hacer timer aca
        _timer -= Time.deltaTime;

        if (_timer <= 0 && aliveNPC > 0)
        {
            _didPlayerWin = true;
            //FadeToBlack();
        }
        //                        |
        // Puede ser no necesario v
        //else if (_timer <= 0 && aliveNPC <= 0)
        //{
        //    FadeToBlack();
        //}
        // Si el timer se acaba y aliveNpC > 0 gana la partida, hacer un fade to black en la pantalla y mostrar una pantalla de win
    }


    private void FadeToBlack()
    {
        _fadeToBlackPanel.LeanAlpha(1, 1f).setOnComplete(ShowFinalScreenPanel);
    }

    private void ShowFinalScreenPanel()
    {
        //Mostrar panel de victoria o derrota segun un booleano (didplayerwin)
        
    }
}

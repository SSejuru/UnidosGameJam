using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup _fadeToBlackPanel;

    private float aliveNPC = 0;
    private bool _didPlayerWin = false;

    public void InitializeManager()
    {
        // Ver cuantos npcs hay vivos al inicio y asignarlos a la variable aliveNPC
        // Setear variables del timer --> variable cuanto va a durar la partida la debemos poder editar desde el inspector
    }

    public void ListenerNPCDeath()
    {
        //NPC ha muerto, entonces hay que disminuir variable de npcs vivos
        // Verificar si estan todos muertos
        // Si estan todos muertos hacer un fade to black en la pantalla y mostrar una pantalla de derrota
    }

    private void Update()
    {
        //Hacer timer aca
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

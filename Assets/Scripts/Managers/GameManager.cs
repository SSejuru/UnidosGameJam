using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    EnemiesManager enemiesManager;

    [SerializeField]
    NPCManager npcManager;

    [SerializeField]
    SpellManager spellManager;

    [SerializeField]
    UIManager uiManager;

    [SerializeField]
    MiniGamesManager miniGamesManager;

    [SerializeField]
    SoundManager soundManager;

    [SerializeField]
    WinConditionManager winConditionManager;

    [SerializeField] private Texture2D cursor;

    private void Awake()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        SetManagers();
    }

    public void SetManagers()
    {
        ManagerLocator.Instance._playerController = playerController;  
        ManagerLocator.Instance._enemiesManager = enemiesManager;
        ManagerLocator.Instance._npcManager = npcManager;
        ManagerLocator.Instance._spellManager = spellManager;
        ManagerLocator.Instance._uiManager = uiManager;
        ManagerLocator.Instance._miniGamesManager = miniGamesManager;
        ManagerLocator.Instance._soundManager = soundManager;
        ManagerLocator.Instance._winConditionManager = winConditionManager;
    }

    public void EndGame()
    {
        Application.Quit();
    }
}

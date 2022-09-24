using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;
   

    private void Awake()
    {
        SetManagers();
    }

    public void SetManagers()
    {
        ManagerLocator.Instance._playerController = playerController;   
    }
}

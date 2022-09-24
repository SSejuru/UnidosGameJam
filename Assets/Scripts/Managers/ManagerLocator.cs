using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLocator 
{
    //Singleton
    private static ManagerLocator _instance;
    public static ManagerLocator Instance
    {
        get
        {
            if (_instance == null) _instance = new ManagerLocator();
            return _instance;
        }
    }

    public ManagerLocator()
    {

    }


    public PlayerController _playerController;
}

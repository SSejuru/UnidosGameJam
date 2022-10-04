using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameCircle : MonoBehaviour
{
    private Minigame _minigameComp;

    private void Start()
    {
        _minigameComp = GetComponent<Minigame>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !_minigameComp.IsOnCooldown)
        {
            ManagerLocator.Instance._playerController.ToggleInteractIcon(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ManagerLocator.Instance._playerController.ToggleInteractIcon(false);
        }
    }

}

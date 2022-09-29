using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField] 
    private float _circleRange;
    [SerializeField]
    private LayerMask _objectMask;

    // Object to interact
    private IInteractable _objectWithInterface;

    public bool _canInteract = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _canInteract)
        {
            SearchInteraction();
        }
    }

    public void ToggleInteraction(bool value)
    {
        _canInteract = value;
    }

    private void SearchInteraction()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, _circleRange, _objectMask);

        float distance = _circleRange;
        bool targetFound = false;

        foreach(Collider2D collider2D in hitColliders)
        {
            if (Vector2.Distance(collider2D.transform.position, transform.position) < distance)
            {
                var interactableObject = collider2D.gameObject.GetComponent<MonoBehaviour>() as IInteractable;

                if (interactableObject != null)
                {
                    distance = Vector2.Distance(collider2D.transform.position, transform.position);
                    _objectWithInterface = interactableObject;
                    targetFound = true;
                }
            }
        }

        if (targetFound)
        {
            _objectWithInterface.Interact();
        }
    }

}

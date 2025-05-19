using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactRadius = 1.0f;
    public LayerMask interactableLayer;

    private IInteractable currentInteractable;
    private IHighlightable currentHighlightable;

    void Update()
    {
        HandleInteractionDetection();
        HandleInteractionInput();
    }

    private void HandleInteractionDetection()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, interactRadius, interactableLayer);

        IInteractable newInteractable = hit ? hit.GetComponent<IInteractable>() : null;
        IHighlightable newHighlightable = hit ? hit.GetComponent<IHighlightable>() : null;

        if (newInteractable != currentInteractable)
        {
            currentHighlightable?.UnHightLight();
            currentInteractable = newInteractable;
            currentHighlightable = newHighlightable;

            if (currentInteractable != null)
            {
                currentHighlightable?.HightLight();
                UIManager.Instance.ShowInteractNotification();
            }
            else
            {
                UIManager.Instance.HideInteractNotification();
            }
        }
    }

    private void HandleInteractionInput()
    {
        if (currentInteractable != null && InputManager.Instance.ISPressInteract())
        {
            currentInteractable.Interact(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
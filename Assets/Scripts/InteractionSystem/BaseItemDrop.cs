using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D),typeof(Rigidbody2D))]
public abstract class BaseItemDrop : MonoBehaviour, IInteractable, IHighlightable
{
    public bool IsPickedUp { get; set; } = false;
    private Material mat;
    private Rigidbody2D rb;
    private void Awake()
    {
        mat = GetComponent<SpriteRenderer>().material;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        rb.simulated = true;
    }

    public void DisableItemDrop()
    {
        rb.simulated = false;
        UnHightLight();
    }
    public void EnableItemDrop()
    {
        rb.simulated = true;
    }

    public void HightLight()
    {
        mat.SetFloat("_Thickness",0.0012f);
    }

    public void UnHightLight()
    {
        mat.SetFloat("_Thickness", 0f);
    }

    public string itemName {  get; set; }
    public Sprite icon { get; set; }
    public abstract void Interact(GameObject interactor);
}
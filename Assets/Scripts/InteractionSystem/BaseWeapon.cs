using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponType
{
    melee,
    gun
}

public class BaseWeapon : BaseItemDrop
{
    float weaponData;
    public Vector3 OriginalLocalScale { get; set; }
    private void Start()
    {
        OriginalLocalScale = transform.localScale;
    }
    private void Update()
    {

    }

    public override void Interact(GameObject interactor)
    {
        WeaponComponent iv = interactor.GetComponent<WeaponComponent>();
        if(iv != null)
        {
            iv.AddItem(this);

            Debug.Log($"{interactor.name} interact with {gameObject.name}");

        }
    }
}

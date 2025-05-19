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

    private void Update()
    {
        RotateWeapon();
    }
    private void RotateWeapon()
    {
        if (!IsPickedUp) return;

        Vector3 mousePos=InputManager.Instance.GetMouseWorldPosition();
        Vector3 toMouse = gameObject.transform.position - mousePos;
        float degree=Mathf.Atan2(toMouse.y, toMouse.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, degree);

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

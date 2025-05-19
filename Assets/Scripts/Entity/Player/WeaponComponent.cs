using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject itemHolder;

    private List<BaseWeapon> inventory = new List<BaseWeapon>(3);
    private int currentSlot = 0;
    public GameObject GetItemHolderPosition() => itemHolder;
    void Start()
    {
        while (inventory.Count < 3)
        {
            inventory.Add(null);
        }
            
    }
    void Update()
    {
        int oldSlot = currentSlot;
        CheckChangeSlot();
        if (currentSlot != oldSlot)
        {
            UpdateActiveStatus();
        }

        CheckAttack();
    }
    private void CheckAttack()
    {
        if (InputManager.Instance.IsMouse1Hold())
        {
            BaseWeapon weapon = inventory[currentSlot];
            if (weapon != null) {
                Debug.Log("attack with" + inventory[currentSlot].name);
            }
            
        }
    }
    public void AddItem(BaseWeapon weapon)
    {
        if (inventory[currentSlot] != null)
        {
            DropItem();
        }
        weapon.DisableItemDrop();
        inventory[currentSlot] = weapon;
        Transform holderTransform = itemHolder.transform;
        weapon.transform.SetParent(holderTransform);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        Vector3 currentScale = weapon.transform.localScale;
        weapon.transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);

        weapon.IsPickedUp = true;

        UpdateActiveStatus();
    }

    public void DropItem() {
        BaseWeapon weapon = inventory[currentSlot];
        if (weapon != null)
        {
            weapon.EnableItemDrop();
            weapon.transform.SetParent(null);
            weapon.IsPickedUp = false;
            inventory[currentSlot] = null;
        }
    }

    private void CheckChangeSlot()
    {
        if (InputManager.Instance.IsPressSlot1()) currentSlot = 0;
        if (InputManager.Instance.IsPressSlot2()) currentSlot = 1;
        if (InputManager.Instance.IsPressSlot3()) currentSlot = 2;
    }
    private void UpdateActiveStatus()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] != null)
            {
                if (i == currentSlot) {
                    inventory[i].gameObject.SetActive(true);
                }
                else
                {
                    inventory[i].gameObject.SetActive(false);
                }

            }

        }
    }
}

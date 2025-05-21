using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class WeaponAimer : MonoBehaviour
{
    [SerializeField] private Transform itemHolder;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        AimAtMouse();
    }

    private void AimAtMouse()
    {
        Vector3 mousePos = InputManager.Instance.GetMouseWorldPosition();
        Vector3 direction = mousePos - transform.position;
        float degree = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (degree > 90 || degree < -90)
        {
            itemHolder.localRotation = Quaternion.Euler(180, 0, -degree);
        }
        else
        {
            itemHolder.localRotation = Quaternion.Euler(0, 0, degree);
        }
    }
}

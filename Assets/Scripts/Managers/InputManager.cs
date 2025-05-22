using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public bool IsMouse1Hold()
    {
        return Input.GetMouseButton(1);
    }
    public bool IsPressSlot1()
    {
        return Input.GetKeyDown(KeyCode.Alpha1);
    }
    public bool IsPressSlot2()
    {
        return Input.GetKeyDown(KeyCode.Alpha2);
    }
    public bool IsPressSlot3()
    {
        return Input.GetKeyDown(KeyCode.Alpha3);
    }

    public float GetInputX()
    {
        return Input.GetAxis("Horizontal");
    }
    public bool ISPressInteract()
    {
        return Input.GetKeyDown(KeyCode.F);
    }
    public bool ISPressJump()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    public Vector3 GetMouseWorldPosition()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 10f;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    public bool IsPressEscape()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
}

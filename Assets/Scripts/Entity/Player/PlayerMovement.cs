using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private InputManager inputManager;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
       

    }

    private void FixedUpdate()
    {

        Move();
        UpdateWeapon();
    }

    private void UpdateWeapon()
    {
        Vector3 mouseWorldPos = inputManager.GetMouseWorldPosition();
        mouseWorldPos.z = 0f;
        Vector2 direction = mouseWorldPos - weapon.transform.position;
        float angleRad = Mathf.Atan2(direction.y, direction.x);
        float angleDeg = angleRad * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.Euler(0, 0, angleDeg);
    }

    private void Move()
    {
        rb.velocity = new Vector2(inputManager.GetInputX() * speed, rb.velocity.y);
    }

    private void FacingLeft()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    private void FacingRight()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}

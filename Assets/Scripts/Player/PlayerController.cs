using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rigidbody2D;

    private Vector2 moveInput = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(moveInput.x * moveSpeed , rigidbody2D.velocity.y);
    }



    public void OnMoveInputAction(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}

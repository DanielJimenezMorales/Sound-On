using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputAction movementAction = null;
    private Vector2 movementVector = Vector2.zero;
    [SerializeField] private float speed = 5f;
    private Transform playerTransform = null;

    private void Awake()
    {
        playerTransform = transform;
    }

    private void OnEnable()
    {
        movementAction.Enable();
    }

    private void OnDisable()
    {
        movementAction.Disable();
    }

    private void Update()
    {
        HandleInput();
        Move();
    }

    private void HandleInput()
    {
        movementVector = movementAction.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 resultMovement = new Vector3(movementVector.x, 0, movementVector.y).normalized;
        playerTransform.Translate(resultMovement * speed * Time.deltaTime);
    }
}

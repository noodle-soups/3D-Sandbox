using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;

    private Vector2 moveVector;
    public int playerHealth = 1;

    [Header("Inputs")]
    private float xInput;
    private float zInput;
    [SerializeField] float moveSpeed;


    void Update()
    {
        movePlayer();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    public void movePlayer()
    {
        Vector3 _movement = new Vector3(moveVector.x, 0f, moveVector.y);

        if (_movement != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_movement), 0.15f);

        transform.Translate(_movement * moveSpeed * Time.deltaTime, Space.World);
    }
}

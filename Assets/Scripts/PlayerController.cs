using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;

    [Header("Inputs")]
    private Vector2 moveVector;
    [SerializeField] float moveSpeed;

    private Camera mainCamera;
    [SerializeField] private LayerMask groundLayer;


    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        movePlayer();
        RotatePlayerToMouse();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    public void movePlayer()
    {
        Vector3 _movement = new Vector3(moveVector.x, 0f, moveVector.y);
        transform.Translate(_movement * moveSpeed * Time.deltaTime, Space.World);
    }

    private void RotatePlayerToMouse()
    {
        Ray _mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_mouseRay, out RaycastHit _raycastHit, float.MaxValue, groundLayer))
        {
            Vector3 _targetPosition = _raycastHit.point;
            // calculate the direction from the player to the target position
            Vector3 _direction = _targetPosition - transform.position;
            // reset y
            _direction.y = 0f;
            // rotate towards _targetPosition
            if (_direction != Vector3.zero)
            {
                Quaternion _targetRotation = Quaternion.LookRotation(_direction);
                Debug.Log(_targetRotation);
                transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, 0.15f);
            }
        }

    }

}

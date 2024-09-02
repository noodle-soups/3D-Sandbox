using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerController : MonoBehaviour
{
    // references
    private CharacterController controller;
    private PlayerControls playerControls;
    private PlayerInput playerInput;
    private Rigidbody rb;

    [Header("States")]
    [SerializeField] private bool isIdle = true;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private bool isDashing = false;

    [Header("Cooldowns")]
    [SerializeField] private bool dashReady = true;

    [Header("Player Properties")]
    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float controllerDeadZone = 0.1f;
    [SerializeField] private float gamepadRotateSmoothing = 1000f;

    [Header("Dash Properties")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private float dashCooldownTimer;
    private Vector3 dashDirection;

    // variables
    private Vector2 movement;
    private Vector2 aim;
    private Vector3 playerVelocity;

    // gamepad
    [SerializeField] private bool isGamepad;

    void Awake()
    {
        // references
        controller = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();

        // state
        isIdle = true;

        // bind inputs
        playerControls.Controls.Dash.performed += _ => StartDash();
    }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void OnDisable()
    {
        playerControls.Disable();
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();
    }

    void HandleInput()
    {
        movement = playerControls.Controls.Movement.ReadValue<Vector2>();
        aim = playerControls.Controls.Aim.ReadValue<Vector2>();
    }

    void HandleMovement()
    {
        // dash movement take priority over normal movement
        if (isDashing)
        {
            controller.Move(dashDirection * Time.deltaTime * dashSpeed);
            return;
        }

        if (rb.velocity == Vector3.zero)
        {
            isIdle = true;
            isMoving = false;
        }
        else if (rb.velocity != Vector3.zero)
        {
            isIdle = false;
            isMoving = true;
        }

        Vector3 move = new Vector3(movement.x, 0, movement.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        // default gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void HandleRotation()
    {
        if (isGamepad)
        {
            if (Mathf.Abs(aim.x) > controllerDeadZone || Mathf.Abs(aim.y) > controllerDeadZone)
            {
                Vector3 playerDirection = Vector3.right * aim.x + Vector3.forward * aim.y;

                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion newRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, gamepadRotateSmoothing * Time.deltaTime);
                }
            }
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(aim);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                LookAt(point);
            }
        }
    }

    void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    void StartDash()
    {
        if (!dashReady) return;
        isDashing = true;
        dashReady = false;
        dashDirection = new Vector3(movement.x, 0, movement.y).normalized;
        StartCoroutine(StopDash());
    }

    IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        dashReady = true;
    }

    public void OnDeviceChange(PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }
}

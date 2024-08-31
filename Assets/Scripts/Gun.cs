using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [Header("References")]
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    private PlayerControls playerControls;

    [Header("Weapon Properties")]
    [SerializeField] private float bulletSpeed;

    [Header("Cooldown")]
    [SerializeField] private float bulletCooldown = 0.1f;
    [SerializeField] private float bulletDestroyTime;

    private Coroutine fireCoroutine; // Store a reference to the firing coroutine

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Controls.Fire1.performed += OnFireStarted;
        playerControls.Controls.Fire1.canceled += OnFireCanceled;
    }

    private void OnDisable()
    {
        playerControls.Controls.Fire1.performed -= OnFireStarted;
        playerControls.Controls.Fire1.canceled -= OnFireCanceled;
        playerControls.Disable();
    }

    private void OnFireStarted(InputAction.CallbackContext context)
    {
        if (fireCoroutine == null) // Start firing only if not already firing
        {
            fireCoroutine = StartCoroutine(FireRoutine());
        }
    }

    private void OnFireCanceled(InputAction.CallbackContext context)
    {
        if (fireCoroutine != null) // Stop firing when the button is released
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    private IEnumerator FireRoutine()
    {
        while (true) // Keep firing while the coroutine is running
        {
            Fire();
            yield return new WaitForSeconds(bulletCooldown); // Wait for the cooldown before allowing another shot
        }
    }

    private void Fire()
    {
        // Instantiate the bullet
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        // Apply bullet momentum
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        // Destroy bullet after time
        Destroy(bullet, bulletDestroyTime);
    }
}

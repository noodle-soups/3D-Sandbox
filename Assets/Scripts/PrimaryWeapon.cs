using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PrimaryWeapon : MonoBehaviour
{
    // references
    private PlayerControls playerControls;
    private PlayerController playerController;

    [Header("Player")]
    [SerializeField] private GameObject player;

    [Header("Prefab")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Weapon State")]
    public bool isWeaponAvailable = true;
    [SerializeField] private bool isWeaponFiring;

    [Header("Cooldown")]
    [SerializeField] private float cooldown = 0.1f;
    [SerializeField] private float cooldownLeft;
    [SerializeField] private float lifetime;

    [Header("Weapon Info")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float playerRotationSpeed;

    // misc.
    private Coroutine fireCoroutine;

    private void Awake()
    {
        // components
        playerController = player.GetComponent<PlayerController>();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Controls.PrimaryWeapon.performed += OnFireStarted;
        playerControls.Controls.PrimaryWeapon.canceled += OnFireCanceled;
    }

    private void OnDisable()
    {
        playerControls.Controls.PrimaryWeapon.performed -= OnFireStarted;
        playerControls.Controls.PrimaryWeapon.canceled -= OnFireCanceled;
        playerControls.Disable();
    }

    private void OnFireStarted(InputAction.CallbackContext context)
    {
        // begin firing only if not already firing
        if (isWeaponAvailable && fireCoroutine == null)
            fireCoroutine = StartCoroutine(FireRoutine());
    }

    private void OnFireCanceled(InputAction.CallbackContext context)
    {
        // stop firing when released
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    private IEnumerator FireRoutine()
    {
        // allow firing while coroutine is active
        while (true)
        {
            Fire();
            // apply cooldown between bullets
            yield return new WaitForSeconds(cooldown);
        }
    }

    private void Fire()
    {
        // instantiate the bullet
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        // apply bullet momentum
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        // destroy bullet after time
        Destroy(bullet, lifetime);
    }
}

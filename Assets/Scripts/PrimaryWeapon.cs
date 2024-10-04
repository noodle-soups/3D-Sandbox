using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PrimaryWeapon : MonoBehaviour
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
    private Coroutine fireCoroutine;

    private void Awake()
    {
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
        if (fireCoroutine == null)
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
            yield return new WaitForSeconds(bulletCooldown);
        }
    }

    private void Fire()
    {
        // instantiate the bullet
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        // apply bullet momentum
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        // destroy bullet after time
        Destroy(bullet, bulletDestroyTime);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PrimaryWeapon : MonoBehaviour
{
    // References and variables
    private PlayerControls playerControls;

    [Header("Weapon State")]
    public bool isWeaponAvailable = true;
    [SerializeField] private bool isFireButtonHeld = false;
    private Coroutine fireCoroutine;

    [Header("Prefab")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Cooldown")]
    [SerializeField] private float cooldown = 0.1f;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float lifetime;

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
        isFireButtonHeld = true;
        if (fireCoroutine == null && isWeaponAvailable)
        {
            fireCoroutine = StartCoroutine(FireRoutine());
        }
    }

    private void OnFireCanceled(InputAction.CallbackContext context)
    {
        isFireButtonHeld = false;
        StopFiring();
    }

    private void StopFiring()
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    public void ResumeFiring()
    {
        StopFiring();  // Ensure the coroutine is fully stopped before resuming

        if (isFireButtonHeld && isWeaponAvailable)
        {
            fireCoroutine = StartCoroutine(FireRoutine());
        }
    }

    private IEnumerator FireRoutine()
    {
        while (isWeaponAvailable)
        {
            Fire();
            yield return new WaitForSeconds(cooldown);
        }
    }

    private void Fire()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        Destroy(bullet, lifetime);
    }
}

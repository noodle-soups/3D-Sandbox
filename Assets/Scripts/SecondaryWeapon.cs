using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeapon : MonoBehaviour
{
    // references
    private PlayerControls playerControls;
    private PlayerController playerController;

    [Header("Player")]
    [SerializeField] private GameObject player;

    [Header("Weaponry")]
    [SerializeField] private Transform weaponsParent;
    [SerializeField] private PrimaryWeapon primaryWeapon;
    public bool primaryWeaponAvailable;

    [Header("Prefab")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    
    [Header("Weapon State")]
    [SerializeField] private bool isWeaponAvailable = true;
    public bool isWeaponFiring;

    [Header("Cooldown")]
    [SerializeField] private float cooldown;
    [SerializeField] private float cooldownLeft;
    [SerializeField] private float lifetime;
    [SerializeField] private float lifetimeLeft;

    [Header("Weapon Info")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float playerRotationSpeed;

    private void Awake()
    {
        // player movements
        playerController = player.GetComponent<PlayerController>();

        // weaponry
        weaponsParent = transform.parent;
        primaryWeapon = weaponsParent.GetComponentInChildren<PrimaryWeapon>();

        // input actions
        playerControls = new PlayerControls();

        // bindings
        playerControls.Controls.SecondaryWeapon.performed += ctx => TryFireSecondaryWeapon();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }


    private void TryFireSecondaryWeapon()
    {
        if (isWeaponAvailable)
        {
            FireSecondaryWeapon();
            StartCoroutine(SecondaryWeaponCooldown());
            StartCoroutine(HandleChanges());
        }
    }

    private void FireSecondaryWeapon()
    {
        var _bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        _bullet.transform.SetParent(bulletSpawnPoint);
        Destroy(_bullet, lifetime);
    }

    private IEnumerator SecondaryWeaponCooldown()
    {
        isWeaponAvailable = false;
        cooldownLeft = cooldown;

        while (cooldownLeft > 0)
        {
            cooldownLeft -= Time.deltaTime;
            yield return null;
        }

        isWeaponAvailable = true;
        cooldownLeft = 0;
    }

    private IEnumerator HandleChanges()
    {
        isWeaponFiring = true;
        ChangeWeaponStates();

        lifetimeLeft = lifetime;
        while (lifetimeLeft > 0)
        {
            lifetimeLeft -= Time.deltaTime;
            playerController.gamepadRotateSmoothing = playerRotationSpeed;
            yield return null;
        }
        lifetimeLeft = 0;

        playerController.gamepadRotateSmoothing = 750f;
        isWeaponFiring = false;
        ChangeWeaponStates();

        primaryWeapon.ResumeFiring();
    }

    private void ChangeWeaponStates()
    {
        primaryWeaponAvailable =! isWeaponFiring;
        primaryWeapon.isWeaponAvailable = primaryWeaponAvailable;
    }

}

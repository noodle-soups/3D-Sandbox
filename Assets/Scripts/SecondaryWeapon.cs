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

    [Header("Prefab")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    
    [Header("Weapon State")]
    [SerializeField] private bool isWeaponAvailable = true;
    [SerializeField] private bool isWeaponFiring;

    [Header("Cooldown")]
    [SerializeField] private float cooldown;
    [SerializeField] private float cooldownLeft;
    [SerializeField] private float lifetime;
    [SerializeField] private float lifetimeLeft;

    [Header("Rotation")]
    [SerializeField] private float weaponRotationSpeed;

    private void Awake()
    {
        // components
        playerController = player.GetComponent<PlayerController>();
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
        lifetimeLeft = lifetime;

        while (lifetimeLeft > 0)
        {
            lifetimeLeft -= Time.deltaTime;
            playerController.gamepadRotateSmoothing = weaponRotationSpeed;
            yield return null;
        }

        playerController.gamepadRotateSmoothing = 750f;
        isWeaponFiring = false;
        lifetimeLeft = 0;
    }

}

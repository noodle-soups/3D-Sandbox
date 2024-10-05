using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeapon : MonoBehaviour
{
    // references
    [SerializeField] private GameObject player;

    private PlayerControls playerControls;
    private PlayerController playerController;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletDestroyTime;
    [SerializeField] private float bulletLifetime;

    [SerializeField] private bool isWeaponAvailable = true;
    [SerializeField] private bool isWeaponFiring;
    [SerializeField] private float cooldownRemaining;
    [SerializeField] private float cooldownTime;

    [SerializeField] private float weaponRotationSpeed;

    private void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
        playerControls = new PlayerControls();
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
        Destroy(_bullet, bulletDestroyTime);
    }

    private IEnumerator SecondaryWeaponCooldown()
    {
        isWeaponAvailable = false;
        cooldownRemaining = cooldownTime;

        while (cooldownRemaining > 0)
        {
            cooldownRemaining -= Time.deltaTime;
            yield return null;
        }

        isWeaponAvailable = true;
        cooldownRemaining = 0;
    }

    private IEnumerator HandleChanges()
    {
        isWeaponFiring = true;
        bulletLifetime = bulletDestroyTime;

        while (bulletLifetime > 0)
        {
            bulletLifetime -= Time.deltaTime;
            Debug.Log(playerController.gamepadRotateSmoothing);
            yield return null;
        }

        isWeaponFiring = false;
        bulletLifetime = 0;
    }

}

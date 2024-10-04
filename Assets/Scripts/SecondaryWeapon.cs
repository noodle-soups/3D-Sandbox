using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeapon : MonoBehaviour
{
    // references
    private PlayerControls playerControls;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletDestroyTime;

    [SerializeField] private bool isWeaponAvailable = true;
    [SerializeField] private float cooldownRemaining;
    [SerializeField] private float cooldownTime;

    private void Awake()
    {
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
        }
    }

    private void FireSecondaryWeapon()
    {
        Debug.Log("Fire secondary weapon");
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
    }

}

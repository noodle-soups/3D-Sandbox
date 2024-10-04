using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeapon : MonoBehaviour
{

    private PlayerControls playerControls;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    [SerializeField] private float bulletDestroyTime;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Controls.SecondaryWeapon.performed += ctx => FireSecondaryWeapon();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }


    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void FireSecondaryWeapon()
    {
        Debug.Log("Fire secondary weapon");

        var _bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        _bullet.transform.SetParent(bulletSpawnPoint);
        Destroy(_bullet, bulletDestroyTime);
    }

}

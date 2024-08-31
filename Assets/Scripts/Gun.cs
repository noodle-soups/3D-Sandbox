using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [Header("References")]
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    [Header("Weapon Properties")]
    [SerializeField] private float bulletSpeed;

    [Header("Cooldown")]
    [SerializeField] private float bulletCooldown = 0.1f;
    [SerializeField] private float bulletDestroyTime;
    private bool isBulletReady = true;
    private float bulletCooldownTimer;

    void Update()
    {
        Fire();
        WeaponCooldown();
    }

    private void Fire()
    {
        if (isBulletReady && Input.GetKey(KeyCode.Space))
        {
            // instatiate the bullet
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            // apply bullet momentum
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            // deactive weapon
            isBulletReady = false;
            // destroy bullet after time
            Destroy(bullet, bulletDestroyTime);
        }
    }

    private void WeaponCooldown()
    {
        if (!isBulletReady)
        {
            // start weapon cooldown
            bulletCooldownTimer += Time.deltaTime;

            // reactivate weapon after cooldown
            if (bulletCooldownTimer > bulletCooldown)
            {
                isBulletReady = true;
                bulletCooldownTimer = 0f;
            }
        }
    }

}

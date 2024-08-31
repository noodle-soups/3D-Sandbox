using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    public float bulletCooldownTimer;
    public float bulletCooldown = 1f;
    public bool isBulletReady = true;

    void Update()
    {

        if (isBulletReady && Input.GetKey(KeyCode.Space))
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Debug.Log("Fire");
            isBulletReady = false;

            bulletCooldownTimer += Time.deltaTime;
        }

        if (!isBulletReady)
        {
            bulletCooldownTimer += Time.deltaTime;
            if (bulletCooldownTimer > bulletCooldown)
            {
                isBulletReady = true;
                bulletCooldownTimer = 0f;
            }
        }

    }
}

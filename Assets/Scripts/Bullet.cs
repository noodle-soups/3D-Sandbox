using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float bulletTimer;
    [SerializeField] private float bulletTimerDestroy;
    public float bulletSpeed = 5f;


    void Update()
    {
        bulletTimer += Time.deltaTime;

        GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;

        if (bulletTimer > bulletTimerDestroy)
            Destroy(gameObject);
    }

}

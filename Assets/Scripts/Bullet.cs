using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float bulletTimer;
    [SerializeField] private float bulletTimerDestroy;


    void Update()
    {
        bulletTimer += Time.deltaTime;
        Debug.Log(bulletTimer);

        if (bulletTimer > bulletTimerDestroy)
            Destroy(gameObject);
    }

}

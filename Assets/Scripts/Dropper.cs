using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    private Rigidbody rb;
    private MeshRenderer mr;

    private float timer;
    [SerializeField] private float dropTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        mr.enabled = false;
    }


    void Update()
    {
        timer = Time.time;

        if (timer > dropTime)
        {
            Debug.Log("Drop now");
            mr.enabled = true;
            rb.useGravity = true;
        }
    }
}

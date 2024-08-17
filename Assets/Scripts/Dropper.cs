using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{

    [SerializeField] private float dropTime;

    void Start()
    {
        
    }


    void Update()
    {
        if (Time.time > dropTime)
            Debug.Log("Drop now");
    }
}

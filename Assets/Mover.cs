using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    void Start()
    {

    }


    void Update()
    {
        float xValue = Input.GetAxisRaw("Horizontal");
        float zValue = Input.GetAxisRaw("Vertical");
        transform.Translate(xValue, 0, zValue);
    }
}

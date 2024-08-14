using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleHit : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

}

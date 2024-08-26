using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleHit : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

}

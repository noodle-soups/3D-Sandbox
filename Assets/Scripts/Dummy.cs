using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player-Secondary-Weapon")
        {
            Debug.Log("HIT!!");
        }
    }

}

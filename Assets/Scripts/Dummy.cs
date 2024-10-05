using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player-Secondary-Weapon")
        {
            Debug.Log("HIT!!!");
        }
    }

}

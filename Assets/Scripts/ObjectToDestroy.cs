using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToDestroy : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player-Bullet")
        {
            Debug.Log("Hit by bullet");
            // destroy game object
            Destroy(gameObject);
            // destroy bullet
            Destroy(collision.gameObject);
        }
    }

}

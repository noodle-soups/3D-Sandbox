using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleHit : MonoBehaviour
{

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.DamagePlayer(1);
            Debug.Log("Hit");
            Debug.Log("Player Health: " + gameManager.playerHealth.ToString());
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

}

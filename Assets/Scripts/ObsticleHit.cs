using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleHit : MonoBehaviour
{

    private GameManager gameManager;
    private PlayerController playerController;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.DamagePlayer(1);
            Debug.Log("Hit");
            Debug.Log("Player Health: " + playerController.playerHealth.ToString());
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int playerHealth = 1;

    public void DamagePlayer(int damageValue)
    {
        playerHealth -= damageValue;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed;
    private Vector2 move;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        movePlayer();
    }

    public void movePlayer()
    {
        Vector3 _movement = new Vector3(move.x, 0f, move.y);

        if (_movement != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_movement), 0.15f);

        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_movement), 0.15f);
        transform.Translate(_movement * speed * Time.deltaTime, Space.World);
        Debug.Log(_movement);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToDestroy : MonoBehaviour
{

    [SerializeField] private bool readyToDestroy = false;
    [SerializeField] private float destroyRotationSpeed;
    [SerializeField] private float destroyShrinkSpeed;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player-Bullet")
        {
            Debug.Log("Hit by bullet");

            readyToDestroy = true;

            // destroy game object
            //Destroy(gameObject);
            
            // destroy bullet
            Destroy(collision.gameObject);
        }
    }

    private void Update()
    {
        if (readyToDestroy)
        {
            
            float _destroyRotationSpeed = destroyRotationSpeed * Time.deltaTime;
            transform.Rotate(0, _destroyRotationSpeed, 0);

            float _destroyShrinkSpeed = destroyShrinkSpeed * Time.deltaTime;
            transform.localScale -= new Vector3(_destroyShrinkSpeed, _destroyShrinkSpeed, _destroyShrinkSpeed);

            if (Mathf.Min(transform.localScale.x, transform.localScale.y, transform.localScale.z) <= 0)
            {
                Destroy(gameObject);
            }

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToDestroy : MonoBehaviour
{

    [SerializeField] private bool readyToDestroy = false;
    [SerializeField] private float destroyRotationSpeed;
    [SerializeField] private float destroyShrinkSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player-Primary-Weapon")
        {
            readyToDestroy = true;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Player-Secondary-Weapon")
        {
            readyToDestroy = true;
        }
    }

    private void Update()
    {
        if (readyToDestroy)
        {
            // rotate
            float _destroyRotationSpeed = destroyRotationSpeed * Time.deltaTime;
            transform.Rotate(0, _destroyRotationSpeed, 0);

            // shrink
            float _destroyShrinkSpeed = destroyShrinkSpeed * Time.deltaTime;
            transform.localScale -= new Vector3(_destroyShrinkSpeed, _destroyShrinkSpeed, _destroyShrinkSpeed);

            // then destroy
            if (Mathf.Min(transform.localScale.x, transform.localScale.y, transform.localScale.z) <= 0)
                Destroy(gameObject);
        }
    }

}

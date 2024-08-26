using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{

    private Camera mainCamera;
    [SerializeField] private LayerMask groundLayer;
    //private Vector3 mousePos;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }


    void Update()
    {
        Ray _mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_mouseRay, out RaycastHit _raycastHit, float.MaxValue, groundLayer))
            transform.position = _raycastHit.point;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField, Range(0, 1000)] private float _force;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                IDamagable damagable = hit.collider.GetComponentInParent<IDamagable>();

                if (damagable != null)
                {
                    Vector3 forceDirection = (hit.point - _camera.transform.position).normalized;
                    forceDirection.y = 0;
                    damagable.TakeDamage(forceDirection * _force, hit.point);
                }
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Range(1f,15f)] private float _force;

    public void Launch(Vector3 endPoint)
    {
        Vector3 direction = endPoint - transform.position;
        GetComponent<Rigidbody>().AddForce(direction.normalized * 100 * _force);
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}

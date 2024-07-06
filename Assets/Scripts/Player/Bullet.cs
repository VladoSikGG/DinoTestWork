using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Range(1f,15f)] private float _force;
    [SerializeField, Range(0, 1000)] private float _forcePullEnemy;
    [SerializeField, Range(0, 100)] private int _damage;

    private Vector3 _startPos;

    public void Launch(Vector3 endPoint)
    {
        _startPos = transform.position;
        Vector3 direction = endPoint - transform.position;
        GetComponent<Rigidbody>().AddForce(direction.normalized * 100 * _force);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponentInParent<IDamagable>();
        
        if (damagable != null)
        {
            Vector3 forceDirection = (transform.position - _startPos).normalized;
            forceDirection.y = 0;
            damagable.TakeDamage(forceDirection * _forcePullEnemy, transform.position, _damage);
        }
        gameObject.SetActive(false);
    }
}

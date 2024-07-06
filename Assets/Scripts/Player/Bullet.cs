using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Range(1f,15f)] private float _force;
    [SerializeField, Range(0f, 1000f)] private float _forcePullEnemy;
    [SerializeField, Range(0, 100)] private int _damage;
    [SerializeField, Range(1f,20f)] private float _lifeTime;

    private Vector3 _startPos;

    private void OnEnable()
    {
        Invoke("Disable", _lifeTime);
    }

    public void Launch(Vector3 endPoint)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        _startPos = transform.position;
        Vector3 direction = (endPoint - transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(direction * _force * 100);
        Debug.Log(direction);
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
        Disable();
    }

    private void Disable() => gameObject.SetActive(false);
}

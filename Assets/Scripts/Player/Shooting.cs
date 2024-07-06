using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Setup Force")]
    [SerializeField, Range(0, 1000)] private float _forcePullEnemy;
    [SerializeField, Range(1,100)] private int _damage;
    [SerializeField, Range(1, 1000)] private float _rayDistance;

    [Header("Pool Setup")]
    [SerializeField, Range(0f, 10f)] private float _spawnOffsetY;
    [SerializeField] private int _bulletCount;
    [SerializeField] private bool _autoExpand;
    [SerializeField] private Transform _containerBullet;
    [SerializeField] private GameObject _bulletPrefab;

    private Camera _camera;
    private ObjectPool<Bullet> _pool;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _pool = new ObjectPool<Bullet>(_bulletPrefab.GetComponent<Bullet>(), _containerBullet, _bulletCount);
        _pool.autoExpand = _autoExpand;
    }

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(_camera.transform.position, ray.direction, Color.green, _rayDistance);
        if (Input.GetMouseButtonDown(0))
        {
            Bullet bullet = CreateBullet();
            
            bullet.Launch(GetEndPoint());
            // if (Physics.Raycast(ray, out RaycastHit hit))
            // {
            //     IDamagable damagable = hit.collider.GetComponentInParent<IDamagable>();
            //
            //     if (damagable != null)
            //     {
            //         Vector3 forceDirection = (hit.point - _camera.transform.position).normalized;
            //         forceDirection.y = 0;
            //         damagable.TakeDamage(forceDirection * _force, hit.point);
            //     }
            // }
        }
    }

    private Vector3 GetEndPoint()
    {
        Vector3 endPoint;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            endPoint = hit.point;
        }
        else
        {
            endPoint = _camera.transform.position + ray.GetPoint(_rayDistance);
        }
        
        Debug.Log(endPoint);
        return endPoint;
    }
    private Bullet CreateBullet()
    {
        var bullet = _pool.GetFreeElement();
        bullet.transform.position = transform.position + new Vector3(0, _spawnOffsetY, 0);
        return bullet;
    }
}

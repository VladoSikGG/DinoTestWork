using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private EnemyView _view;
    [SerializeField] private RaagdollDeath _raagdollDeath;
    [SerializeField] private int _health;

    private void Awake()
    {
        _view.Initialize();
        _raagdollDeath.Initialize();
    }

    private void Kill(Vector3 force, Vector3 hitPoint)
    {
        EnableRagdollBehaviour();
        _raagdollDeath.Hit(force, hitPoint);
    }

    public void TakeDamage(Vector3 force, Vector3 hitPoint, int damage)
    {
         _health -= damage;
         if (_health <= 0) Kill(force, hitPoint);
    }

    private void EnableRagdollBehaviour()
    {
        _view.DisableAnimator();
        _raagdollDeath.Enable();
    }
}

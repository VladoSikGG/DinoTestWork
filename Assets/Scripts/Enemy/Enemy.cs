using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private EnemyView _view;
    [SerializeField] private RaagdollDeath _raagdollDeath;

    private void Awake()
    {
        _view.Initialize();
        _raagdollDeath.Initialize();
    }

    private void Kill()
    {
        EnableRagdollBehaviour();
        
    }

    public void TakeDamage(Vector3 force, Vector3 hitPoint)
    {
        EnableRagdollBehaviour();
        _raagdollDeath.Hit(force, hitPoint);
    }

    private void EnableRagdollBehaviour()
    {
        _view.DisableAnimator();
        _raagdollDeath.Enable();
    }
}

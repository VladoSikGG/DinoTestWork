using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float _speedRotation;
    private StageManager _stageManager;
    private PlayeMovement _playeMovement;
    private void Start()
    {
        _stageManager = GameObject.Find("StageManagerEnemy").GetComponent<StageManager>();
        _playeMovement = GetComponent<PlayeMovement>();
    }

    private void Update()
    {
        if (_stageManager.GetFirstEnemy() == null) return;
        
        if (_playeMovement.IsMoving == false) 
        {
            Transform target = _stageManager.GetFirstEnemy();
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
        
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, _speedRotation);
        }
        
    }
}

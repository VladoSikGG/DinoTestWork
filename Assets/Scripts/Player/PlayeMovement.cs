using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayeMovement : MonoBehaviour
{
    [HideInInspector] public List<Vector3> wayPoints;
    
    [SerializeField] private GameObject _wayPointObj;
    
    private bool _canMove;
    
    //components
    private NavMeshAgent _agent;

    private void Awake()
    {
        InitializeComponents();
    }

    private void Start()
    {
        _canMove = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_canMove) GoToNextWayPoint();
        }
        
    }

    private void InitializeComponents()
    {
        _agent = GetComponent<NavMeshAgent>();
        for (int i = 0; i < _wayPointObj.transform.childCount; i++)
        {
            wayPoints.Add(_wayPointObj.transform.GetChild(i).transform.position);
        }
    }

    private void GoToNextWayPoint()
    {
        _agent.SetDestination(wayPoints[0]);
        wayPoints.RemoveAt(0);
        _canMove = false;
    }

    public void ReadyToMove()
    {
        _canMove = true;
    }
}

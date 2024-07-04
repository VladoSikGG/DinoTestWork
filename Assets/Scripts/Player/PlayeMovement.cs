using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayeMovement : MonoBehaviour
{
    [SerializeField] private GameObject _wayPointObj;
    
    public List<Vector3> _wayPoints;
    public bool _canMove;
    
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

        if (_agent.hasPath ==false)
        {
            _canMove = true;
        }
    }

    private void InitializeComponents()
    {
        _agent = GetComponent<NavMeshAgent>();
        for (int i = 0; i < _wayPointObj.transform.childCount; i++)
        {
            _wayPoints.Add(_wayPointObj.transform.GetChild(i).transform.position);
        }
    }

    private void GoToNextWayPoint()
    {
        _agent.SetDestination(_wayPoints[0]);
        _wayPoints.RemoveAt(0);
        _canMove = false;
    }
}

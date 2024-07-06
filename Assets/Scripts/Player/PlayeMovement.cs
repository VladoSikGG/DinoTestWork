using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayeMovement : MonoBehaviour
{
    private const string KEY_IS_RUN = "IsRun";
    [HideInInspector] public List<Vector3> wayPoints;
    
    [SerializeField] private GameObject _wayPointObj;

    private bool _startGame, _isMoving;
    
    //components
    private Animator _animator;
    private NavMeshAgent _agent;

    public bool IsMoving => _isMoving;
    private void Awake()
    {
        InitializeComponents();
    }

    private void Start()
    {
        _startGame = true;
        GetComponent<Shooting>().enabled = false;
    }

    private void Update()
    {
        if (_startGame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GoToNextWayPoint();
                GetComponent<Shooting>().enabled = true;
            }
        }

        if (_agent.hasPath == false)
        {
            IdleAnimation();
            _isMoving = false;
        }
        else
        {
            _isMoving = true;
            RunAnimation();
        }
    }

    private void InitializeComponents()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        for (int i = 0; i < _wayPointObj.transform.childCount; i++)
        {
            wayPoints.Add(_wayPointObj.transform.GetChild(i).transform.position);
        }
    }

    public void GoToNextWayPoint()
    {
        _agent.SetDestination(wayPoints[0]);
        wayPoints.RemoveAt(0);
        
        _startGame = false;
    }

    private void RunAnimation() => _animator.SetBool(KEY_IS_RUN, true);
    private void IdleAnimation() => _animator.SetBool(KEY_IS_RUN, false);
}

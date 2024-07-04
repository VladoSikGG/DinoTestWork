using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] public List<Transform> enemyStage;

    [SerializeField] private PlayeMovement _playeMovement;

    private bool _stageIsClear;

    private void Update()
    {
        if (enemyStage[0].childCount <= 0)
        {
            _stageIsClear = true;
            enemyStage.RemoveAt(0);
            _playeMovement.ReadyToMove();
        }
    }

    private void InitializeObjects()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            enemyStage.Add(transform.GetChild(i));
        }
    }
}

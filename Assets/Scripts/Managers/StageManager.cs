using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public List<Transform> enemyStage;

    [SerializeField] private GameObject _winPanel;
    [SerializeField] private PlayeMovement _playeMovement;

    private bool _stageIsClear;

    private void Start()
    {
        InitializeObjects();
        for (int i = 1; i < enemyStage.Count; i++)
            enemyStage[i].gameObject.SetActive(false);
    }

    private void Update()
    {
        if (enemyStage.Count <= 0)
        {
            _winPanel.SetActive(true);
            _playeMovement.GetComponent<Shooting>().enabled = false;
            return;
        }
        
        if (enemyStage[0].childCount <= 0)
        {
            _stageIsClear = true;
            enemyStage.RemoveAt(0);
            _playeMovement.GoToNextWayPoint();
            
            if (enemyStage.Count <= 0) return;
            
            enemyStage[0].gameObject.SetActive(true);
        }
    }

    private void InitializeObjects()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            enemyStage.Add(transform.GetChild(i));
        }
    }

    public Transform GetFirstEnemy()
    {
        if (enemyStage.Count > 0)
            return enemyStage[0].GetChild(0);
        
        return null;
    }
}

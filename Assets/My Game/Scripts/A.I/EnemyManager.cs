using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharacterManager
{
    EnemyLocomotionManager enemyLocomotion;
    public bool isPerformingAction;

    [Header("A.I Settings")]
    public float detectionRadius = 20;
    public float minimumDetectionAngle = -50;
    public float maximumDetectionAngle = 50;


    private void Awake()
    {
        enemyLocomotion = GetComponent<EnemyLocomotionManager>();
        
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        HandleCurrentActions();


    }

    private void HandleCurrentActions()
    {
        if (enemyLocomotion.currentTarget == null)
        {
            enemyLocomotion.HandleDetection();
        }
        else
        {
            enemyLocomotion.HandleMoveToTarget();
        }
    }
}

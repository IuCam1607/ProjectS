using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : CharacterManager
{
    public EnemyLocomotionManager enemyLocomotion;
    public EnemyAnimatorManager enemyAnimator;
    public EnemyStatsManager enemyStats;
    public bool isPerformingAction;

    public State currentState;
    public CharacterStatsManager currentTarget;

    public NavMeshAgent navMeshAgent;
    public Rigidbody enemyRigidbody;



    public float rotationSpeed = 15;
    public float maximumAttackRange = 1.5f;

    [Header("Combat Flags")]
    public bool canDoCombo;

    [Header("A.I Settings")]
    public float detectionRadius = 20;
    public float minimumDetectionAngle = -50;
    public float maximumDetectionAngle = 50;

    [Header("A.I Combat Settings")]
    public bool allowAIToPerformCombos;
    public float comboLikelyHood;

    [Header("Flags")]
    public bool applyRootMotion = false;
    public bool canRotate = false;
    public bool canMove = false;
    public bool isInteracting;

    public float currentRecoveryTime = 0;


    private void Awake()
    {
        enemyLocomotion = GetComponent<EnemyLocomotionManager>();
        enemyAnimator = GetComponent<EnemyAnimatorManager>();
        enemyStats = GetComponent<EnemyStatsManager>();
        enemyRigidbody = GetComponent<Rigidbody>();
        backStabCollider = GetComponentInChildren<CriticalDamageCollider>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        navMeshAgent.enabled = false;
    }

    private void Start()
    {
        enemyRigidbody.isKinematic = false;
    }

    private void Update()
    {
        HandleRecoveryTimer();
        HandleStateMachine();

        isInteracting = enemyAnimator.animator.GetBool("IsInteracting");
        enemyAnimator.animator.SetBool("isDead", enemyStats.isDead);
        canDoCombo = enemyAnimator.animator.GetBool("canDoCombo");
        if (enemyStats.isDead)
        {
            enemyRigidbody.isKinematic = true;
            navMeshAgent.enabled = false;
        }
    }

    private void LateUpdate()
    {
        navMeshAgent.transform.localPosition = Vector3.zero;
        navMeshAgent.transform.localRotation = Quaternion.identity;
    }


    private void HandleStateMachine()
    {
        if (currentState != null)
        {
            State nextState = currentState.Tick(this, enemyStats, enemyAnimator);

            if (nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    private void SwitchToNextState(State state)
    {
        currentState = state;
    }

    private void HandleRecoveryTimer()
    {
        if (currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if (isPerformingAction)
        {
            if (currentRecoveryTime <= 0)
            {
                isPerformingAction = false;
            }
        }
    }

    #region Attacks



    #endregion
}

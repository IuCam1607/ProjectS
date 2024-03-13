using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : CharacterManager
{
    public EnemyLocomotionManager enemyLocomotion;
    public EnemyAnimatorManager enemyAnimator;
    public EnemyStatsManager enemyStats;

    public State currentState;
    public CharacterStatsManager currentTarget;

    public NavMeshAgent navMeshAgent;
    public Rigidbody enemyRigidbody;

    public float rotationSpeed = 25;
    public float maximumAggroRadius = 1.5f;

    [Header("A.I Settings")]
    public float detectionRadius = 20;
    public float minimumDetectionAngle = -50;
    public float maximumDetectionAngle = 50;

    [Header("A.I Combat Settings")]
    public bool allowAIToPerformCombos;
    public float comboLikelyHood;
    public float currentRecoveryTime = 0;
    public bool isPhaseShifting;

    protected override void Awake()
    {
        base.Awake();
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

        if (enemyStats.isBoss)
        {
            isPhaseShifting = enemyAnimator.animator.GetBool("isPhaseShifting");
        }

        isUsingLeftHand = enemyAnimator.animator.GetBool("isUsingLeftHand");
        isUsingRightHand = enemyAnimator.animator.GetBool("isUsingRightHand");
        isRotatingWithRootMotion = enemyAnimator.animator.GetBool("isRotatingWithRootMotion");
        isInteracting = enemyAnimator.animator.GetBool("isInteracting");      
        isInvulnerable = enemyAnimator.animator.GetBool("isInvulnerable");
        canDoCombo = enemyAnimator.animator.GetBool("canDoCombo");
        canRotate = enemyAnimator.animator.GetBool("canRotate");
        enemyAnimator.animator.SetBool("isDead", enemyStats.isDead);
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

}

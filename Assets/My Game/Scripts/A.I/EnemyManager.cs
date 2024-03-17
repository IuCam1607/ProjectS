using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : CharacterManager
{
    public EnemyLocomotionManager enemyLocomotion;
    public EnemyAnimatorManager enemyAnimator;
    public EnemyStatsManager enemyStats;
    public EnemyBossManager enemyBossManager;
    public FeedBackManager feedBackManager;
    public GameObject combatCollider;
    public GameObject combatTransform;
    public CapsuleCollider enemyCollider;

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
        enemyBossManager = GetComponent<EnemyBossManager>();
        enemyLocomotion = GetComponent<EnemyLocomotionManager>();
        enemyAnimator = GetComponent<EnemyAnimatorManager>();
        enemyStats = GetComponent<EnemyStatsManager>();
        enemyRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        feedBackManager = GetComponent<FeedBackManager>();
        enemyCollider = GetComponent<CapsuleCollider>();
        feedBack = GetComponent<MMF_Player>();
        sfx = feedBack.GetFeedbackOfType<MMF_MMSoundManagerSound>();

        backStabCollider = GetComponentInChildren<CriticalDamageCollider>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        navMeshAgent.enabled = false;
        animator.applyRootMotion = true;
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
            isPhaseShifting = animator.GetBool("isPhaseShifting");
        }

        isUsingLeftHand = animator.GetBool("isUsingLeftHand");
        isUsingRightHand = animator.GetBool("isUsingRightHand");
        isRotatingWithRootMotion = animator.GetBool("isRotatingWithRootMotion");
        isInteracting = animator.GetBool("isInteracting");      
        isInvulnerable = animator.GetBool("isInvulnerable");
        canDoCombo = animator.GetBool("canDoCombo");
        canRotate = animator.GetBool("canRotate");
        animator.SetBool("isDead", isDead);
        if (isDead)
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
            State nextState = currentState.Tick(this);

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

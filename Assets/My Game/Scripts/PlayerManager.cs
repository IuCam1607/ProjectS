using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public PlayerAnimationManager playerAnimationManager;
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public PlayerLocomotion playerLocomotion;
    [HideInInspector] public PlayerInputManager playerInput;
    [HideInInspector] public Animator animator;

    [Header("Flags")]
    public bool isPerformingAction = false;
    public bool applyRootMotion = false;
    public bool canRotate = true;
    public bool canMove = true;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        characterController = GetComponent<CharacterController>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerInput = GetComponent<PlayerInputManager>();
        animator = GetComponent<Animator>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();

    }
    private void Start()
    {
        //PlayerInputManager.instance.player = this;
    }
    private void Update()
    {
        playerInput.HandleAllInputs();
    }
    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
    }
    private void LateUpdate()
    {
        PlayerCamera.instance.HandleAllCameraActions();
    }
}

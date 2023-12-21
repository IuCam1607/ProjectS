using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    PlayerLocomotionManager playerLocomotionManager;

    protected override void Awake()
    {
        base.Awake();

        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
    }

    protected override void Update()
    {
        base.Update();

        if (!IsOwner)
            return;

        playerLocomotionManager.HandleAllMovement();
    }
    protected override void LateUpdate()
    {
        if(!IsOwner)
            return;
        if (IsOwner)
        {
            PlayerCamera.instance.player = this;
        }

        base.LateUpdate();

        PlayerCamera.instance.HandleAllCameraActions();
    }
    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        if(IsOwner)
        {
            PlayerCamera.instance.player = this;
        }
    }
}

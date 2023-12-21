using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;

public class CharacterManager : NetworkBehaviour
{
    public CharacterController characterController;

    CharacterNetworkManager characterNetworkManager;

    [SerializeField]private float gravity = 9.8f;
    [SerializeField]private float verticalSpeed;

    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);

        characterController = GetComponent<CharacterController>();
        characterNetworkManager = GetComponent<CharacterNetworkManager>();
    }
    protected virtual void Update()
    {
        if (IsOwner)
        {
            characterNetworkManager.networkPosition.Value = transform.position;
            characterNetworkManager.networkRotation.Value = transform.rotation;
        }
        else
        {
            transform.position = Vector3.SmoothDamp
                (transform.position,
                characterNetworkManager.networkPosition.Value,
                ref characterNetworkManager.networkPositionVelocity,
                characterNetworkManager.networkPositionSmoothTime);

            transform.rotation = Quaternion.Slerp(transform.rotation,
                characterNetworkManager.networkRotation.Value,
                characterNetworkManager.networkRotationSmoothTime);
        }
        //test
        if (!characterController.isGrounded)
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }
        else
        {
            // Reset tốc độ di chuyển khi đối tượng đứng trên mặt đất
            verticalSpeed = 0f;
        }

        // Di chuyển đối tượng theo trục y
        Vector3 moveDirection = new Vector3(0, verticalSpeed, 0);
        characterController.Move(moveDirection * Time.deltaTime);
    }
    protected virtual void LateUpdate()
    {
        
    }
}

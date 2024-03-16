using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;

    public Camera cameraObject;
    public PlayerManager player;
    public Transform cameraPivotTransform;

    [Header("Camera Setting")]
    [SerializeField] private float cameraSmoothSpeed = 1;
    [SerializeField] private float leftAndRightRotationSpeed;
    [SerializeField] private float upAndDownRotationSpeed;
    [SerializeField] private float minimunPivot = -30;
    [SerializeField] private float maximunPivot = 60;
    [SerializeField] private float cameraCollisionRadius = 0.2f;
    [SerializeField] private float lockedPivotPosition = 2.25f;
    [SerializeField] private float unlockedPivotPosition = 1.65f;
    [SerializeField] public LayerMask collideWithLayers;
    [SerializeField] public LayerMask environmentLayer;

    [Header("Camera Values")]
    private Vector3 cameraVelocity;
    private Vector3 cameraObjectPosition;
    [SerializeField] private float leftAndRightLookAngle;
    [SerializeField] private float upAndDownLookAngle;
    private float cameraZPosition;
    private float targetCameraZPosition;

    [Header("Lock On")]
    [SerializeField] private float lockOnRadius = 26;
    [SerializeField] private float maximumLockOnDistance = 30;
    public List<CharacterManager> availableTargets = new List<CharacterManager> ();
    public CharacterManager nearestLockOnTarget;
    public CharacterManager currentLockOnTarget;
    public CharacterManager leftLockTarget;
    public CharacterManager rightLockTarget;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        player = FindAnyObjectByType<PlayerManager>();
    }
    private void Start()
    {
        cameraZPosition = cameraObject.transform.position.z;
        environmentLayer = LayerMask.NameToLayer("Environment");
    }

    private void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerManager>();
        }
    }

    public void HandleAllCameraActions()
    {
        if(player != null)
        {
            HandleFollowTarget();
            HandleRotations();
            HandleCollision();
        }
    }
    
    private void HandleFollowTarget()
    {
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetCameraPosition;
    }
    private void HandleRotations()
    {
        if (player.playerInput.isLockedOn == false && currentLockOnTarget == null)
        {
            leftAndRightLookAngle += player.playerInput.cameraHorizontalInput * leftAndRightRotationSpeed * Time.deltaTime;

            upAndDownLookAngle -= player.playerInput.cameraVerticalInput * upAndDownRotationSpeed * Time.deltaTime;
            upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimunPivot, maximunPivot);

            Vector3 cameraRotation;
            Quaternion targetRotation;

            cameraRotation = Vector3.zero;
            cameraRotation.y = leftAndRightLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            transform.rotation = targetRotation;

            cameraRotation = Vector3.zero;
            cameraRotation.x = upAndDownLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            cameraPivotTransform.localRotation = targetRotation;
        }
        else
        {
            float velocity = 0;
            currentLockOnTarget = nearestLockOnTarget;

            if(nearestLockOnTarget == null)
            {
                player.playerInput.isLockedOn = false;
                return;
            }

            Vector3 direction = currentLockOnTarget.transform.position - transform.position;
            direction.Normalize();
            direction.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;

            direction = currentLockOnTarget.transform.position - cameraPivotTransform.position;
            direction.Normalize();

            targetRotation = Quaternion.LookRotation(direction);
            Vector3 eulerAngle = targetRotation.eulerAngles;
            eulerAngle.y = 0;
            cameraPivotTransform.localEulerAngles = eulerAngle;
        }

    }
    private void HandleCollision()
    {
        targetCameraZPosition = cameraZPosition;
        RaycastHit hit;
        Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition), collideWithLayers))
        {
            float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
        }

        if(Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
        {
            targetCameraZPosition = -cameraCollisionRadius;
        }

        cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
        cameraObject.transform.localPosition = cameraObjectPosition;
    }

    public void HandleLockOn()
    {
        float shortestDistance = Mathf.Infinity;
        float shortestDistanceOfLeftTarget = -Mathf.Infinity;
        float shortestDistanceOfRightTarget = Mathf.Infinity;

        Collider[] colliders = Physics.OverlapSphere(player.transform.position, lockOnRadius);

        if (colliders.Length == 0)
        {
            return;
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterManager character = colliders[i].GetComponent<CharacterManager>();

            if (character != null)
            {
                Vector3 lockTargetDirection = character.transform.position - player.transform.position;
                float distanceFromTarget = Vector3.Distance(player.transform.position, character.transform.position);
                float viewableAngle = Vector3.Angle(lockTargetDirection, cameraObject.transform.forward);
                RaycastHit hit;

                if (character.transform.root != player.transform.root
                    && viewableAngle > -50 && viewableAngle < 50
                    && distanceFromTarget <= maximumLockOnDistance)
                {
                    if (Physics.Linecast(player.lockOnTransform.position, character.lockOnTransform.position, out hit))
                    {
                        Debug.DrawLine(player.lockOnTransform.position, character.lockOnTransform.position);

                        if (hit.transform.gameObject.layer == environmentLayer)
                        {

                        }
                        else
                        {
                            availableTargets.Add(character);
                        }
                    }
                } 
            }
        }

        for (int k = 0; k < availableTargets.Count; k++)
        {
            float distanceFromTarget = Vector3.Distance(player.transform.position, availableTargets[k].transform.position);
            
            if (distanceFromTarget < shortestDistance)
            {
                shortestDistance = distanceFromTarget;
                nearestLockOnTarget = availableTargets[k];
            }

            if (player.playerInput.isLockedOn)
            {
                if(currentLockOnTarget != null)
                {
                    //Vector3 relativeEnemyPosition = currentLockOnTarget.transform.InverseTransformPoint(availableTargets[k].transform.position);
                    //var distanceFromLeftTarget = currentLockOnTarget.transform.position.x - availableTargets[k].transform.position.x;
                    //var distanceFromRightTarget = currentLockOnTarget.transform.position.x + availableTargets[k].transform.position.x;

                    Vector3 relativeEnemyPosition = player.playerInput.transform.InverseTransformPoint(availableTargets[k].transform.position);
                    var distanceFromLeftTarget = relativeEnemyPosition.x;
                    var distanceFromRightTarget = relativeEnemyPosition.x;

                    if (relativeEnemyPosition.x <= 0.00 && distanceFromLeftTarget > shortestDistanceOfLeftTarget 
                        && availableTargets[k] != currentLockOnTarget)
                    {
                        shortestDistanceOfLeftTarget = distanceFromLeftTarget;
                        leftLockTarget = availableTargets[k];
                    }

                    else if (relativeEnemyPosition.x >= 0.00 && distanceFromRightTarget < shortestDistanceOfRightTarget
                        && availableTargets[k] != currentLockOnTarget)
                    {
                        shortestDistanceOfRightTarget = distanceFromRightTarget;
                        rightLockTarget = availableTargets[k];
                    }
                }

            }
        }
    }

    public void ClearLockOnTargets()
    {
        availableTargets.Clear();
        currentLockOnTarget = null;
        nearestLockOnTarget = null;
    }

    public void SetCameraHeight()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 newLockedPosition = new Vector3(0, lockedPivotPosition);
        Vector3 newUnlockedPosition = new Vector3(0, unlockedPivotPosition);

        if (currentLockOnTarget != null)
        {
            cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition,
                newLockedPosition, ref velocity, Time.deltaTime);
        }
        else
        {
            cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition,
                newUnlockedPosition, ref velocity, Time.deltaTime);
        }
    }
}

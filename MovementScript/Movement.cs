using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Required Components
[RequireComponent(typeof(GroundedState))]
[RequireComponent(typeof(MovementChange))]
[RequireComponent(typeof(JumpingState))]
[RequireComponent(typeof(ChangeState))]
[RequireComponent(typeof(AnimationChange))]
[RequireComponent(typeof(IKSystem))]
[RequireComponent(typeof(FallingState))]
[RequireComponent(typeof(CameraZoom))]
[RequireComponent(typeof(StaminaSystem))]
[RequireComponent(typeof(AttackingScript))]
[RequireComponent(typeof(PlayerInteract))]
[RequireComponent(typeof(PlayerInvCPL))]
#endregion
public class Movement : MonoBehaviour
{
    [SerializeField] public FixedJoystick jy_;

    #region Camera Fields
    [SerializeField] public CinemachineVirtualCamera VirtualCamera;
    [SerializeField] public Transform MainCamera;
    #endregion

    #region Player Fields
    [SerializeField] public CharacterController charControl;
    [SerializeField] public Animator animator;
    [SerializeField] public LayerMask layerMasks;
    #endregion

    #region Player Values
    // On Water Physics
    [SerializeField] public float waterLevel = 4f;
    [SerializeField] public float underwaterGravity = -3f;
    // Gravity Application
    [SerializeField] public float Gravity = -9.81f;

    // Player Movement
    [SerializeField] public float playerSpeed = 3.5f;
    [SerializeField] public float speedModifier = 1.5f;
    [SerializeField] public float slopeSpeedModifier = 0.5f;
    [SerializeField] public Vector3 Velocity; // Might be used for some things

    // Jump Movement
    [SerializeField] public float jumpForce = 4.4f;
    [SerializeField] public float runningForce = 3.2f;
    [SerializeField] public float sprintForce = 4.2f;

    // Player Ground detection
    [SerializeField] public float FallingHeightDiff = 1.5f;
    [SerializeField] public float slopeAngle;

    // Player Turn Smoothing
    [SerializeField] private float turnSmoothing = 0.1f;
    [SerializeField] private float smoothingVelocity;
    #endregion

    #region Player State Bools
    // Player State
    [SerializeField] public string playerState;
    [SerializeField] public bool isMoving;
    [SerializeField] public bool isInteracting;

    // Player Movement Types
    [SerializeField] public bool isWalking;
    [SerializeField] public bool isRunning;
    [SerializeField] public bool isSprinting;
    [SerializeField] public bool isJumping;
    [SerializeField] public bool isDashing;
    [SerializeField] public bool isAttacking;

    // Attacking State
    [SerializeField] public string attackType;

    // On Water States
    [SerializeField] public bool isSwimming;

    // Airborne States
    [SerializeField] public bool isFalling;

    // Grounded State
    [SerializeField] public bool isGrounded;
    [SerializeField] public bool onSlope;
    [SerializeField] public bool goingUp;
    [SerializeField] public bool goingDown;

    // Others
    [SerializeField] public bool onInventory;

    // Can State
    [SerializeField] public bool canSwim;
    [SerializeField] public bool canSprint;
    [SerializeField] public bool canDash;
    [SerializeField] public bool canMove;
    [SerializeField] public bool canJump;
    [SerializeField] public bool canAttack;
    #endregion

    #region Stamina & Oxygen System
    [SerializeField] public float maxStamina = 120f;
    [SerializeField] public float maxOxygen = 90f;
    [SerializeField] public float currentOxygen;
    [SerializeField] public float currentStamina;
    [SerializeField] public float RegenRate = 9f;
    [SerializeField] public float DecreaseRate = 12f;
    #endregion

    private void Awake()
    {
        charControl = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        isRunning = true;
        canSprint = true;
        canSwim = true;
        canDash = true;
        canMove = true;

        // Hiding the Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float horizontal = Input.GetKey(KeyCode.A) ? -1f : Input.GetKey(KeyCode.D) ? 1f : jy_.Horizontal;
        float vertical = Input.GetKey(KeyCode.W) ? 1f : Input.GetKey(KeyCode.S) ? -1f : jy_.Vertical;

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        if (direction.magnitude >= 0.1f)
        {
            if (canMove)
            {
                if (!isInteracting)
                {
                    isMoving = true;
                    if (!isAttacking)
                    {
                        if (!isFalling)
                        {
                            if (!isJumping)
                            {
                                if (canSwim)
                                {
                                    // This Section will calculate the direction of the player, then smoothens it rotation based on the calulated direction
                                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + MainCamera.eulerAngles.y;
                                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothingVelocity, turnSmoothing);
                                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                                    float newSpeed = playerSpeed * speedModifier;

                                    Vector3 newDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                                    charControl.Move(newDirection.normalized * newSpeed * Time.deltaTime);
                                }
                                else
                                {
                                    isMoving = false;
                                }
                            }

                            if (isJumping && isSprinting)
                            {
                                float newSpeed = playerSpeed * speedModifier;

                                Vector3 newDirection = transform.forward;
                                charControl.Move(newDirection.normalized * newSpeed * Time.deltaTime);
                            }
                        }
                    }
                }
                else
                {
                    isMoving = false;
                    return;
                }
            }
            else
            {
                isMoving = false;
                return;
            }
        }
        else
        {
            isMoving = false;
        }
    }
}

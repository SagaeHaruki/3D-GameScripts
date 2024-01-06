using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class IKSystem : MonoBehaviour
{
    Movement playerMovement;

    #region Some Vars here
    [SerializeField] public string playerState;
    #endregion

    #region booleans
    [SerializeField] private bool useProIK = false;
    [SerializeField] private bool startIK;
    [SerializeField] private bool enableFeetIk = true;
    [SerializeField] private bool showDebugs = true;
    #endregion

    #region All about the foot ik system
    [Range(0, 2)][SerializeField] public float pelvisLowerOffset = 0.05f;
    [SerializeField] private float heightFromGround = 0.45f;
    [SerializeField] private float raycastDownDistance = 0.45f;
    [SerializeField] private float pelvisOffset = 0f;
    [SerializeField] private float pelvisUpDownSpeed = 0.8f;
    [SerializeField] private float feetIkPosSpeed = 0.12f;
    #endregion

    #region Some useful things
    private Vector3 leftFootPos, leftFootIkPos;
    private Vector3 rightFootPos, rightFootIkPos;
    private Quaternion leftFootIkRot, rightFootIkRot;
    private float lastPelvisPosY, lastLeftFotPosY, lastRightFotPosY;
    #endregion

    #region Curves for the animator
    public string leftFootAnim = "LeftFootCurve";
    public string rightFootAnim = "RightFootCurve";
    #endregion

    #region Slopes
    private float maxRayDistance = 1.0f;
    private float slopeAngle;
    #endregion

    private void Awake()
    {
        playerMovement = GetComponent<Movement>();
        enableFeetIk = true;
    }

    private void Update()
    {
        ChangeState();
        GetSlopeAngle();
    }

    private void GetSlopeAngle()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit, maxRayDistance, playerMovement.layerMasks))
        {
            Vector3 groundNormal = hit.normal;
            slopeAngle = Vector3.Angle(groundNormal, Vector3.up);

        }
    }


    private void ChangeState()
    {
        if (playerMovement.isMoving)
        {
            if (slopeAngle >= 40 && slopeAngle <= 45)
            {
                heightFromGround = 0.67f;
                raycastDownDistance = 0.67f;
            }
            else if (slopeAngle >= 35 && slopeAngle <= 40)
            {
                heightFromGround = 0.62f;
                raycastDownDistance = 0.62f;
            }
            else if (slopeAngle >= 30 && slopeAngle <= 35)
            {
                heightFromGround = 0.57f;
                raycastDownDistance = 0.57f;
            }
            else if (slopeAngle >= 25 && slopeAngle <= 30)
            {
                heightFromGround = 0.52f;
                raycastDownDistance = 0.52f;
            }
            else if (slopeAngle >= 20 && slopeAngle <= 25)
            {
                heightFromGround = 0.47f;
                raycastDownDistance = 0.47f;
            }
            else if(playerMovement.isJumping || !playerMovement.charControl.isGrounded)
            {
                heightFromGround = 0.0f;
                raycastDownDistance = 0.0f;
            }
            else
            {
                heightFromGround = 0.43f;
                raycastDownDistance = 0.43f;
            }
        }
        else
        {
            heightFromGround = 0.43f;
            raycastDownDistance = 0.43f;
        }
    }

    #region Ground
    private void FixedUpdate()
    {
        if (!playerMovement.isSwimming)
        {
            if (!playerMovement.isJumping)
            {
                if (playerMovement.onSlope || !playerMovement.isMoving)
                {
                    if (enableFeetIk == false)
                    {
                        return;
                    }

                    if (playerMovement.animator == null)
                    {
                        return;
                    }

                    AdjustTargetFeet(ref rightFootPos, HumanBodyBones.RightFoot);
                    AdjustTargetFeet(ref leftFootPos, HumanBodyBones.LeftFoot);

                    FeetPositionSolver(rightFootPos, ref rightFootIkPos, ref rightFootIkRot);
                    FeetPositionSolver(leftFootPos, ref leftFootIkPos, ref leftFootIkRot);
                }
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!playerMovement.isSwimming)
        {
            if (!playerMovement.isJumping)
            {
                if (playerMovement.onSlope || !playerMovement.isMoving)
                {
                    if (enableFeetIk == false)
                    {
                        return;
                    }
                    if (playerMovement.animator == null)
                    {
                        return;
                    }
                    MovePelvisHeight();

                    // Right Foot ik Position & Rotation
                    playerMovement.animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
                    if (useProIK)
                    {
                        playerMovement.animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, playerMovement.animator.GetFloat(rightFootAnim));
                    }
                    MoveToIKPoint(AvatarIKGoal.RightFoot, rightFootIkPos, rightFootIkRot, ref lastRightFotPosY);

                    // Left Foot ik Position & Rotation
                    playerMovement.animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
                    if (useProIK)
                    {
                        playerMovement.animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, playerMovement.animator.GetFloat(leftFootAnim));
                    }
                    MoveToIKPoint(AvatarIKGoal.LeftFoot, leftFootIkPos, leftFootIkRot, ref lastLeftFotPosY);
                }
            }
        }
    }
    #endregion

    #region FootGround

    private void MoveToIKPoint(AvatarIKGoal foot, Vector3 posIKHolder, Quaternion rotIKHolder, ref float lastFootPosY)
    {
        if (!playerMovement.isSwimming)
        {
            if (!playerMovement.isJumping)
            {
                if (playerMovement.onSlope || !playerMovement.isMoving)
                {
                    Vector3 targetIkPos = playerMovement.animator.GetIKPosition(foot);
                    if (posIKHolder != Vector3.zero)
                    {
                        targetIkPos = transform.InverseTransformPoint(targetIkPos);
                        posIKHolder = transform.InverseTransformPoint(posIKHolder);
                        float yVar = Mathf.Lerp(lastFootPosY, posIKHolder.y, feetIkPosSpeed);
                        targetIkPos.y += yVar;
                        lastFootPosY = yVar;
                        targetIkPos = transform.TransformPoint(targetIkPos);
                        playerMovement.animator.SetIKRotation(foot, rotIKHolder);
                    }
                    playerMovement.animator.SetIKPosition(foot, targetIkPos);
                }
            }
        }
    }

    private void MovePelvisHeight()
    {
        if (!playerMovement.isSwimming)
        {
            if (!playerMovement.isJumping)
            {
                if (playerMovement.onSlope || !playerMovement.isMoving)
                {
                    if (rightFootIkPos == Vector3.zero || leftFootIkPos == Vector3.zero || lastPelvisPosY == 0)
                    {
                        lastPelvisPosY = playerMovement.animator.bodyPosition.y;
                        return;
                    }
                    float leftOffsetPos = leftFootIkPos.y - transform.position.y;
                    float rightOffsetPos = rightFootIkPos.y - transform.position.y;
                    float totalOffsetVal = (leftOffsetPos < rightOffsetPos) ? leftOffsetPos : rightOffsetPos;

                    if (!playerMovement.isMoving)
                    {
                        Vector3 newPelvisPos = playerMovement.animator.bodyPosition + Vector3.up * totalOffsetVal;
                        newPelvisPos.y = Mathf.Lerp(lastPelvisPosY, newPelvisPos.y, pelvisUpDownSpeed);
                        playerMovement.animator.bodyPosition = newPelvisPos;
                        lastPelvisPosY = playerMovement.animator.bodyPosition.y;
                    }
                    else
                    {
                        Vector3 upDirection = Vector3.up;
                        RaycastHit hit;

                        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
                        {
                            upDirection = hit.normal; // Get the surface normal where the character is standing
                        }

                        float slopeAngle = Vector3.Angle(Vector3.up, upDirection);
                        float pelvisOffset = Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * totalOffsetVal - pelvisLowerOffset;

                        Vector3 newPelvisPos = playerMovement.animator.bodyPosition + Vector3.up * pelvisOffset;
                        newPelvisPos.y = Mathf.Lerp(lastPelvisPosY, newPelvisPos.y, pelvisUpDownSpeed);
                        playerMovement.animator.bodyPosition = newPelvisPos;
                        lastPelvisPosY = playerMovement.animator.bodyPosition.y;
                    }
                }
            }
        }
    }

    private void FeetPositionSolver(Vector3 fromSkyPos, ref Vector3 ikFeetPos, ref Quaternion feetIkRot)
    {
        if (!playerMovement.isSwimming)
        {
            if (!playerMovement.isJumping)
            {
                if (playerMovement.onSlope || !playerMovement.isMoving)
                {
                    // Raycasting Section
                    RaycastHit footHit;
                    if (showDebugs)
                    {
                        Debug.DrawLine(fromSkyPos, fromSkyPos + Vector3.down * (raycastDownDistance + heightFromGround), Color.yellow);
                    }

                    if (Physics.Raycast(fromSkyPos, Vector3.down, out footHit, raycastDownDistance + heightFromGround, playerMovement.layerMasks))
                    {
                        ikFeetPos = fromSkyPos;
                        ikFeetPos.y = footHit.point.y + pelvisOffset;
                        feetIkRot = Quaternion.FromToRotation(Vector3.up, footHit.normal) * transform.rotation;
                        return;
                    }
                    ikFeetPos = Vector3.zero;
                }
            }
        }
    }

    private void AdjustTargetFeet(ref Vector3 feetPositions, HumanBodyBones foot)
    {
        if (!playerMovement.isSwimming)
        {
            if (!playerMovement.isJumping)
            {
                if (playerMovement.onSlope || !playerMovement.isMoving)
                {
                    feetPositions = playerMovement.animator.GetBoneTransform(foot).position;
                    feetPositions.y = transform.position.y + heightFromGround;
                }
            }
        }
    }
    #endregion
}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    Movement playerMovement;
    PlayerInvCPL playercpl;

    #region Value: Camera Scroll zomm & Camera zoom smoothness
    [SerializeField] private CinemachineFramingTransposer transposer;
    [SerializeField] private CinemachinePOV pov;
    [SerializeField] private float defaultDistance = 6.0f;
    [SerializeField] private float currentDistance;
    [SerializeField] public float currentTargetDistance;
    [SerializeField] private float minDistance = 1.0f;
    [SerializeField] private float maxDistance = 6.0f;
    [SerializeField] private float mouseX_sens = 1.5f;
    [SerializeField] private float mouseY_sens = 1.5f;

    [SerializeField] private float smoothing = 4f;
    [SerializeField] private float zoomSensitivity = 1f;
    [SerializeField] public bool vcamToggle;

    #endregion

    private void Awake()
    {
        playerMovement = GetComponent<Movement>();
        playercpl = GetComponent<PlayerInvCPL>();
        transposer = playerMovement.VirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        pov = playerMovement.VirtualCamera.GetCinemachineComponent<CinemachinePOV>();

        currentTargetDistance = defaultDistance;
        vcamToggle = true;
    }

    private void Update()
    {
        GetKeyInpts();

        if (vcamToggle && !playerMovement.onInventory)
        {
            // Default Mouse Sensitivity
            pov.m_VerticalAxis.m_MaxSpeed = mouseY_sens;
            pov.m_HorizontalAxis.m_MaxSpeed = mouseX_sens;

            // Camera Zoom
            float zoomInpt = Input.GetAxis("Mouse ScrollWheel");

            float zoomValue = -zoomInpt * zoomSensitivity;

            currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, minDistance, maxDistance);

            currentDistance = transposer.m_CameraDistance;

            if (currentDistance == currentTargetDistance)
            {
                return;
            }

            float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, smoothing * Time.deltaTime);

            transposer.m_CameraDistance = lerpedZoomValue;
        }
    }

    private void GetKeyInpts()
    {
        if (playerMovement.onInventory)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            playerMovement.canAttack = false;
            vcamToggle = false;

            // Toggle Virtual Camera
            pov.m_VerticalAxis.m_MaxSpeed = 0f;
            pov.m_HorizontalAxis.m_MaxSpeed = 0f;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                // Enables the cursor
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                playerMovement.canAttack = false;
                vcamToggle = false;

                // Toggle Virtual Camera
                pov.m_VerticalAxis.m_MaxSpeed = 0f;
                pov.m_HorizontalAxis.m_MaxSpeed = 0f;
            }
            else
            {
                // Disables the cursor
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                playerMovement.canAttack = true;
                vcamToggle = true;

                // Toggle Virtual camera
                pov.m_VerticalAxis.m_MaxSpeed = mouseY_sens;
                pov.m_HorizontalAxis.m_MaxSpeed = mouseX_sens;
            }
        }
    }
}

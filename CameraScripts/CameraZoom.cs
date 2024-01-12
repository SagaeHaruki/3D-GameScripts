using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    Movement playerMovement;

    #region Value: Camera Scroll zomm & Camera zoom smoothness
    [SerializeField] private CinemachineFramingTransposer transposer;
    [SerializeField] private CinemachinePOV pov;
    [SerializeField] private float defaultDistance = 6.0f;
    [SerializeField] private float minDistance = 1.0f;
    [SerializeField] private float maxDistance = 6.0f;
    [SerializeField] private float mouseX_sens = 1.5f;
    [SerializeField] private float mouseY_sens = 1.5f;

    [SerializeField] private float smoothing = 4f;
    [SerializeField] private float zoomSensitivity = 1f;
    [SerializeField] private bool vcamToggle;

    private float currentTargetDistance;

    #endregion

    private void Awake()
    {
        playerMovement = GetComponent<Movement>();
        transposer = playerMovement.VirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        pov = playerMovement.VirtualCamera.GetCinemachineComponent<CinemachinePOV>();

        currentTargetDistance = defaultDistance;
        vcamToggle = true;
    }

    private void Update()
    {
        GetKeyInpts();

        if (vcamToggle)
        {
            // Default Mouse Sensitivity
            pov.m_VerticalAxis.m_MaxSpeed = mouseY_sens;
            pov.m_HorizontalAxis.m_MaxSpeed = mouseX_sens;

            // Camera Zoom
            float zoomInpt = Input.GetAxis("Mouse ScrollWheel");

            float zoomValue = -zoomInpt * zoomSensitivity;

            currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, minDistance, maxDistance);

            float currentDistance = transposer.m_CameraDistance;

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
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            // Enables the cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            playerMovement.allowAttack = false;
            playerMovement.canDash = false;
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

            playerMovement.allowAttack = true;
            playerMovement.canDash = true;
            vcamToggle = true;

            // Toggle Virtual camera
            pov.m_VerticalAxis.m_MaxSpeed = mouseY_sens;
            pov.m_HorizontalAxis.m_MaxSpeed = mouseX_sens;
        }
    }
}

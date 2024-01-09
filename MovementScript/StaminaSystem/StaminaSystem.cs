using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    Movement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<Movement>();

        playerMovement.currentOxygen = playerMovement.maxOxygen;
        playerMovement.currentStamina = playerMovement.maxStamina;
    }

    private void Update()
    {
        if (playerMovement == null) 
        {
            return;
        }

        StaminaRate();
        OxygenRate();
    }

    private void StaminaRate()
    {
        // Stamina Depletion when sprinting but not swimming
        if (playerMovement.isSprinting && !playerMovement.isSwimming && playerMovement.isMoving)
        {
            playerMovement.currentStamina -= playerMovement.DecreaseRate * Time.deltaTime;
        }

        // Stamina Regeneration
        if (!playerMovement.isSprinting || playerMovement.isSwimming)
        {
            playerMovement.currentStamina = Mathf.Min(playerMovement.maxStamina, playerMovement.currentStamina + playerMovement.RegenRate * Time.deltaTime);
        }

        if (playerMovement.currentStamina <= 0)
        {
            playerMovement.currentStamina = 0;
            playerMovement.canSprint = false;
            playerMovement.isSprinting = false;
        }

        if (playerMovement.currentStamina >= 30)
        {
            playerMovement.canSprint = true;
        }
    }

    private void OxygenRate()
    {
        // Oxygen Depletion when swimming and moving
        if (playerMovement.isSwimming && playerMovement.isMoving)
        {
            playerMovement.currentOxygen -= playerMovement.DecreaseRate * Time.deltaTime;
        }

        // Oxygen Regeneration
        if (!playerMovement.isSwimming || !playerMovement.isMoving)
        {
            playerMovement.currentOxygen = Mathf.Min(playerMovement.maxOxygen, playerMovement.currentOxygen + playerMovement.RegenRate * Time.deltaTime);
        }

        if (playerMovement.currentOxygen <= 0)
        {
            playerMovement.currentOxygen = 0;
            playerMovement.canSwim = false;
            playerMovement.isMoving = false;
        }

        if (playerMovement.currentOxygen >= 30)
        { 
            playerMovement.canSwim = true;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackingScript : MonoBehaviour
{
    Movement playerMovement;

    [SerializeField] private float attacktime;
    [SerializeField] private float attackCooldown = 0.7f;
    [SerializeField] private bool canAttack;

    private void Awake()
    {
        playerMovement = GetComponent<Movement>();
        canAttack = true;
    }

    private void Update()
    {
        if (playerMovement == null) 
        {
            return;
        }

        PressAttackState();
        ResetAttacking();

        if (playerMovement.isAttacking)
        {
            float interactRange = 2.2f;

            NPC_1 closestNPC = null;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            float closestDistance = float.MaxValue;

            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out NPC_1 npcInteract))
                {
                    float distance = Vector3.Distance(transform.position, npcInteract.transform.position);

                    if (distance < closestDistance)
                    {
                        closestNPC = npcInteract;
                        closestDistance = distance;

                        Vector3 direction = closestNPC.transform.position - transform.position;
                        direction.y = 0;
                        Quaternion targetRot = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 15f * Time.deltaTime);
                    }
                }
            }
        }
    }

    private void PressAttackState()
    {
        if (playerMovement.canAttack)
        {
            if (!playerMovement.isInteracting)
            {
                if (Input.GetMouseButtonDown(0) && canAttack)
                {
                    playerMovement.isDashing = false;
                    playerMovement.isSprinting = false;
                    playerMovement.isAttacking = true;
                    attacktime = 0f;
                    StartCoroutine(AttackCooldowns());

                    if (playerMovement.attackType == "")
                    {
                        playerMovement.attackType = "Attack1";
                    }
                    else if (playerMovement.attackType == "Attack1")
                    {
                        playerMovement.attackType = "Attack2";
                    }
                    else if (playerMovement.attackType == "Attack2")
                    {
                        playerMovement.attackType = "Attack3";
                    }
                    else if (playerMovement.attackType == "Attack3")
                    {
                        playerMovement.attackType = "Attack1";
                    }
                }
            }
        }
    }

    IEnumerator AttackCooldowns()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void ResetAttacking()
    {
        if (!playerMovement.isMoving)
        {
            if (playerMovement.isAttacking)
            {
                attacktime += Time.deltaTime;
                if (attacktime >= 1.5f)
                {
                    playerMovement.isAttacking = false;
                    playerMovement.attackType = "";
                    attacktime = 0f;
                }
            }
        }

        if(playerMovement.isMoving)
        {
            if (playerMovement.isAttacking)
            {
                attacktime += Time.deltaTime;
                if (attacktime >= 0.9f)
                {
                    playerMovement.isAttacking = false;
                    playerMovement.attackType = "";
                    attacktime = 0f;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_1 : MonoBehaviour
{
    private Animation animator;
    DialogueBox dialogueBox;

    [SerializeField] public string NPCName;
    [SerializeField] public string NPCProfession;
    [SerializeField] private string[] NPCDialogues;
    private int currentDialogue = 0;
    public bool isInteracting;
    private GameObject player;

    private void Awake()
    {
        dialogueBox = GameObject.Find("DialogueBox").GetComponent<DialogueBox>();
        animator = GetComponent<Animation>();
        player = GameObject.Find("Player");

        this.dialogueBox.npc_Name.text = NPCName;
        this.dialogueBox.npc_Profession.text = NPCProfession;
        if(this.NPCDialogues.Length > 0)
        {
            this.dialogueBox.npc_Dialogue.text = NPCDialogues[0];
        }
    }

    public void Interact()
    {
        if (currentDialogue < NPCDialogues.Length - 1)
        {
            currentDialogue++;
            this.dialogueBox.npc_Dialogue.text = NPCDialogues[currentDialogue];
            dialogueBox.Interacted(NPCName, NPCProfession);
            isInteracting = true;
        }
        else if (currentDialogue == NPCDialogues.Length - 1)
        {
            dialogueBox.CloseInteract();
            currentDialogue = 0;
            isInteracting = false;
        }
    }

    public void Update()
    {
        if (isInteracting)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0;
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 2.5f * Time.deltaTime);
        }
        else
        {
            Vector3 direction = new Vector3(120f, 0f, 0f);
            direction.y = 0;
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 2.5f * Time.deltaTime);
        }
    }
}

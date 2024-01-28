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

    private void Awake()
    {
        dialogueBox = GameObject.Find("DialogueBox").GetComponent<DialogueBox>();
        animator = GetComponent<Animation>();
        dialogueBox.npc_Name.text = NPCName;
        if(this.NPCDialogues.Length > 0)
        {
            dialogueBox.npc_Dialogue.text = NPCDialogues[0];
        }
    }

    public void Interact()
    {
        if (currentDialogue < NPCDialogues.Length - 1)
        {
            currentDialogue++;
            dialogueBox.npc_Dialogue.text = NPCDialogues[currentDialogue];
            dialogueBox.Interacted();
            isInteracting = true;
        }
        else if (currentDialogue == NPCDialogues.Length - 1)
        {
            dialogueBox.CloseInteract();
            currentDialogue = 0;
            isInteracting = false;
        }
    }
}

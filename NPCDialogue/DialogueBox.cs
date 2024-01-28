using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] public GameObject dialogueBox;
    [SerializeField] public TMP_Text npc_Name;
    [SerializeField] public TMP_Text npc_Dialogue;

    private void Awake()
    {
        dialogueBox.gameObject.SetActive(false);
    }

    public void Interacted()
    {
        dialogueBox.gameObject.SetActive(true);
    }

    public void CloseInteract()
    {
        dialogueBox.gameObject.SetActive(false);
    }
}

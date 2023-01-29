using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : Event
{
    private DialogueManager dialogueManager = GameManager.Instance.dialogueManager;
   
    // The ID is the index of the dialoguesequence that needs to be played from the array dialogueSequences in the dialogueManager
    public DialogueEvent(int _id) : base(_id) 
    {
        dialogueManager = gameManager.dialogueManager;    
    }

    public override void Run()
    {
        base.Run();
        Debug.Log("Running dialogue event " + ID);
        dialogueManager.RunDialogueSequence(dialogueManager.dialogueSequences[ID], this);
    }
}

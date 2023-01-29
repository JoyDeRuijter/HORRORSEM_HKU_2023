using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private DialogueSequence[] dialogueSequences;

    [HideInInspector] public bool pressedSpace;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            pressedSpace = true;

        // Start the first dialoguesequence as test
        if (Input.GetKeyDown(KeyCode.P))
            StartCoroutine(PlayDialogueSequence(dialogueSequences[0]));
    }

    public IEnumerator PlayDialogueSequence(DialogueSequence _dialogueSequence)
    {
        for (int i = 0; i < _dialogueSequence.dialogueObjects.Length; i++)
        {
            dialogueUI.ShowDialogue(_dialogueSequence.dialogueObjects[i]);
            yield return new WaitUntil(() => pressedSpace == true && _dialogueSequence.dialogueObjects[i].isDone);
            pressedSpace = false;
        }

        dialogueUI.CloseDialogueBox();
    }
}

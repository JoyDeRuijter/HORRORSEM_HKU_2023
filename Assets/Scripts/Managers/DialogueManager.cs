using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public DialogueSequence[] dialogueSequences;
    
    [HideInInspector] public bool pressedSpace;
    
    [SerializeField] private DialogueUI dialogueUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            pressedSpace = true;
    }

    private IEnumerator PlayDialogueSequence(DialogueSequence _dialogueSequence, DialogueEvent _thisEvent)
    {
        GameManager.Instance.player.dialogueIsPlaying = true;

        for (int i = 0; i < _dialogueSequence.dialogueObjects.Length; i++)
        {
            dialogueUI.ShowDialogue(_dialogueSequence.dialogueObjects[i]);
            yield return new WaitUntil(() => pressedSpace == true && _dialogueSequence.dialogueObjects[i].isDone);
            pressedSpace = false;
        }
        Debug.Log("Finish running dialogueEvent " + _thisEvent.ID);
        _thisEvent.SetToDone();
        dialogueUI.CloseDialogueBox();
        GameManager.Instance.player.dialogueIsPlaying = false;
    }

    public void RunDialogueSequence(DialogueSequence _dialogueSequence, DialogueEvent _thisEvent)
    {
        StartCoroutine(PlayDialogueSequence(_dialogueSequence, _thisEvent));
    }
}

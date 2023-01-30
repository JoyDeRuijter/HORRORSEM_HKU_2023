using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class FirstSequence : Sequence
{
    public override void Initialize()
    {
        ID = 0;
        state = SequenceState.WAITING;
        gameManager = GameManager.Instance;
    }

    public override bool IsTriggered()
    {
        gameManager = GameManager.Instance;
        Debug.Log("GameManager: " + gameManager);
        if (gameManager.playerRoomBlock != null)
        { 
            Debug.Log("PlayerRoomBlock: " + gameManager.playerRoomBlock);
            Debug.Log("PlayerRoomBlockID: " + gameManager.playerRoomBlock.ID);
        }

        // Replace later with task is done?
        if (gameManager.playerRoomBlock != null && gameManager.playerRoomBlock.ID == 9)
        {
            Debug.Log("IS TRIGGERED");
            return true;
        }
        return false;
    }

    public override IEnumerator Run()
    {
        DialogueEvent niceGrandmaDialogue = new DialogueEvent(0);
        niceGrandmaDialogue.Run();
        // start new task
        state = SequenceState.DONE;
        yield return null;
    }
}

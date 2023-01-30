using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class FirstSequence : Sequence
{
    public FirstSequence()
    {
        ID = 0;
        state = SequenceState.WAITING;
        gameManager = GameManager.Instance;
    }

    public override bool IsTriggered()
    {
        gameManager = GameManager.Instance;

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
        gameManager.houseManager.automaticLights = true;
        // start new task
        state = SequenceState.DONE;
        yield return null;
    }
}

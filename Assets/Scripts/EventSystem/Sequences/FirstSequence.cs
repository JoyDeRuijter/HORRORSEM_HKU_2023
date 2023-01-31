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

        if (gameManager.playerRoomBlock != null && gameManager.playerRoomBlock.ID == 9)
            return true;

        return false;
    }

    public override IEnumerator Run()
    {
        yield return new WaitForSeconds(0.15f);
        DialogueEvent niceGrandmaDialogue = new DialogueEvent(0);
        niceGrandmaDialogue.Run();
        yield return new WaitUntil(() => niceGrandmaDialogue.State == EventState.DONE);
        gameManager.taskManager.StartNewTask(1);
        yield return new WaitUntil(() => gameManager.playerRoomBlock != null && gameManager.playerRoomBlock.ID == 2);
        yield return new WaitForSeconds(6f);
        gameManager.taskManager.taskOneComplete = true;
        state = SequenceState.DONE;
        yield return null;
    }
}

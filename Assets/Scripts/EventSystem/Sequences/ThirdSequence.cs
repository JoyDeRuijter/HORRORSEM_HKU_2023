using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdSequence : Sequence
{
    public ThirdSequence()
    {
        ID = 2;
        state = SequenceState.WAITING;
        gameManager = GameManager.Instance;
    }

    public override bool IsTriggered()
    {
        gameManager = GameManager.Instance;

        if (gameManager.playerRoomBlock != null && gameManager.sequenceManager.sequences.Peek() == this)
            return true;

        return false;
    }

    public override IEnumerator Run()
    {
        Debug.Log("RUN THIRD SEQUENCE");
        yield return null;
        state = SequenceState.DONE;
    }
}

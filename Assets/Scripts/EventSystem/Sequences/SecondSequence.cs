using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondSequence : Sequence
{
    public SecondSequence()
    {
        ID = 1;
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
        DialogueEvent paintingGrandmaDialogue = new DialogueEvent(1);
        paintingGrandmaDialogue.Run();
        yield return new WaitUntil(() => paintingGrandmaDialogue.State == EventState.DONE);
        gameManager.houseManager.automaticLights = false;
        LightEvent lightsOffEvent = new LightEvent(0, LightEventType.ALLOFF);
        lightsOffEvent.Run();
        DialogueEvent lightsOffGrandmaDialogue = new DialogueEvent(2);
        lightsOffGrandmaDialogue.Run();
        yield return new WaitUntil(() => lightsOffGrandmaDialogue.State == EventState.DONE);
        gameManager.taskManager.StartNewTask(3);
        yield return new WaitUntil(() => gameManager.taskManager.tasks[3].isCompleted == true);
        gameManager.player.flashLight.enabled = true;
        gameManager.player.flashLight.TurnOnFlashLight();
        RoomEvent switchLivingroomEvent = new RoomEvent(9, 3, RoomEventType.SWITCH);
        DialogueEvent whereIsGrandmaDialogue = new DialogueEvent(3);
        gameManager.grandma.SetActive(false);
        whereIsGrandmaDialogue.Run();
        gameManager.taskManager.StartNewTask(4);
    }
}

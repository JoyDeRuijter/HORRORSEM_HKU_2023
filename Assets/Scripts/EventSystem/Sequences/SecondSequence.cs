using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

        if (gameManager.playerRoomBlock != null
            && gameManager.sequenceManager.sequences.Peek() == this
            && gameManager.taskManager.tasks[1].isCompleted)
            return true;

        return false;
    }

    public override IEnumerator Run()
    {
        Debug.Log("RUN SECOND SEQUENCE");
        yield return new WaitUntil(() => gameManager.taskManager.tasks[1].isCompleted == true);
        yield return new WaitForSeconds(3f);
        gameManager.taskManager.StartNewTask(2);
        yield return new WaitForSeconds(0.5f);
        gameManager.taskManager.ManuallyUpdateToUncompleted();
        Debug.Log("THIIIISSSS: CURRENTTASK.ISCOMPLETED: " + gameManager.taskManager.currentTask.isCompleted);
        Debug.Log("THIIIISSSS: SPRITE: " + gameManager.taskManager.image.sprite);
        yield return new WaitUntil(() => gameManager.taskManager.tasks[2].isCompleted == true);
        yield return new WaitUntil(() => gameManager.playerRoomBlock.ID == 9);
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
        yield return new WaitForSeconds(0.2f);
        gameManager.taskManager.ManuallyUpdateToUncompleted();
        yield return new WaitUntil(() => gameManager.playerRoomBlock.ID == 7);
        yield return new WaitForSeconds(0.5f);
        gameManager.player.flashLight.enabled = true;
        gameManager.player.flashLight.TurnOnFlashLight();
        RoomEvent switchLivingroomEvent = new RoomEvent(9, 3, RoomEventType.SWITCH);
        switchLivingroomEvent.Run();
        DialogueEvent whereIsGrandmaDialogue = new DialogueEvent(3);
        gameManager.grandma.SetActive(false);
        whereIsGrandmaDialogue.Run();
        yield return new WaitUntil(() => whereIsGrandmaDialogue.State == EventState.DONE);
        gameManager.taskManager.StartNewTask(4);
        gameManager.taskManager.ManuallyUpdateToUncompleted();
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => gameManager.playerRoomBlock.ID == 9);
        yield return new WaitForSeconds(0.2f);
        gameManager.taskManager.taskFourComplete = true;
        state = SequenceState.DONE;
    }
}

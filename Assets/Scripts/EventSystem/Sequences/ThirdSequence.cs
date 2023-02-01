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
        RoomEvent roomEvent1 = new RoomEvent(4, 7, RoomEventType.SWITCH);
        roomEvent1.Run();
        DoorEvent doorEvent1 = new DoorEvent(4, false);
        doorEvent1.Run();
        yield return new WaitForSeconds(4f);
        
        DialogueEvent dialogueEventWhat = new DialogueEvent(4);
        dialogueEventWhat.Run();
        yield return new WaitUntil(() => dialogueEventWhat.State == EventState.DONE);
        gameManager.taskManager.StartNewTask(5);
        gameManager.taskManager.ManuallyUpdateToUncompleted();
        yield return new WaitUntil(() => gameManager.playerRoomBlock.ID == 3);
        yield return new WaitForSeconds(0.2f);
        gameManager.taskManager.taskFiveComplete = true;
        
        DialogueEvent dialogueEventLosingMind = new DialogueEvent(5);
        dialogueEventLosingMind.Run();
        gameManager.grandma.transform.position = new Vector3(4.5f, -0.83f, 0);
        gameManager.grandma.SetActive(true);
        yield return new WaitUntil(() => dialogueEventLosingMind.State == EventState.DONE);
        yield return new WaitForSeconds(0.2f);
        LightEvent flickerBathroom = new LightEvent(6, LightEventType.FLICKER);
        flickerBathroom.Run();
        yield return new WaitForSeconds(1.5f);
        flickerBathroom.Run();
        yield return new WaitForSeconds(0.2f);
        
        DialogueEvent dialogueEventGRANDMA = new DialogueEvent(6);
        dialogueEventGRANDMA.Run();
        DoorEvent doorEvent2 = new DoorEvent(0, false);
        doorEvent2.Run();
        RoomEvent roomEvent2 = new RoomEvent(4, 8, RoomEventType.SWITCH);
        roomEvent2.Run();
        RoomEvent roomEvent3 = new RoomEvent(7, 5, RoomEventType.SWITCH);
        roomEvent3.Run();
        DoorEvent doorEvent3 = new DoorEvent(3, false);
        doorEvent3.Run();
        yield return new WaitUntil(() => dialogueEventGRANDMA.State == EventState.DONE);
        gameManager.grandma.SetActive(false);
        gameManager.taskManager.StartNewTask(6);
        gameManager.taskManager.ManuallyUpdateToUncompleted();
        yield return new WaitUntil(() => gameManager.playerRoomBlock.ID == 5);
        yield return new WaitForSeconds(0.5f);
        gameManager.taskManager.taskSixComplete = true;
        
        DialogueEvent dialogueEventDoor = new DialogueEvent(7);
        dialogueEventDoor.Run();
        yield return new WaitUntil(() => dialogueEventDoor.State == EventState.DONE);
        gameManager.taskManager.StartNewTask(7);
        gameManager.taskManager.ManuallyUpdateToUncompleted();
        yield return new WaitUntil(() => gameManager.playerRoomBlock.ID == 2);
        gameManager.taskManager.taskSevenComplete = true;
        yield return new WaitForSeconds(0.8f);
        
        DialogueEvent dialogueEventPainting = new DialogueEvent(8);
        dialogueEventPainting.Run();
        DoorEvent doorEvent4 = new DoorEvent(4, true);
        doorEvent4.Run();
        RoomEvent roomEvent4 = new RoomEvent(3, 5, RoomEventType.SWITCH);
        roomEvent4.Run();
        DoorEvent doorEvent5 = new DoorEvent(1, false);
        doorEvent5.Run();
        DoorEvent doorEvent6 = new DoorEvent(3, true);
        doorEvent6.Run();
        RoomEvent roomEvent5 = new RoomEvent(6, 10, RoomEventType.SWITCH);
        roomEvent5.Run();
        gameManager.grandma.SetActive(true);
        gameManager.grandma.transform.Rotate(0, 0, 90);
        gameManager.grandma.transform.position = new Vector3(0.8f, -2.6f, 0);
        yield return new WaitUntil(() => dialogueEventPainting.State == EventState.DONE);
        yield return new WaitForSeconds(1f);
        LightEvent flickerBedroom = new LightEvent(9, LightEventType.FLICKER);
        flickerBedroom.Run();
        yield return new WaitForSeconds(1f);
        LightEvent turnOnBedroom = new LightEvent(9, LightEventType.SLOWON);
        turnOnBedroom.Run();
        
        DialogueEvent dialogueEventGrandmaTired = new DialogueEvent(9);
        dialogueEventGrandmaTired.Run();
        yield return new WaitUntil(() => dialogueEventGrandmaTired.State == EventState.DONE);
        flickerBedroom.Run();
        
        DialogueEvent dialogueEventConfusion = new DialogueEvent(10);
        dialogueEventConfusion.Run();
        yield return new WaitUntil(() => dialogueEventConfusion.State == EventState.DONE);
        gameManager.grandma.SetActive(false);
        gameManager.taskManager.StartNewTask(8);
        gameManager.taskManager.ManuallyUpdateToUncompleted();
        yield return new WaitForSeconds(0.5f);
        LightEvent flickerStairs = new LightEvent(6, LightEventType.FLICKER);
        flickerStairs.Run();
        yield return new WaitForSeconds(0.5f);
        flickerStairs.Run();
        RoomEvent roomEvent6 = new RoomEvent(3, 8, RoomEventType.SWITCH);
        roomEvent6.Run();
        yield return new WaitUntil(() => gameManager.playerRoomBlock.ID == 4);
        LightEvent flickerOtherStairs = new LightEvent(8, LightEventType.FLICKER);
        flickerOtherStairs.Run();
        yield return new WaitForSeconds(0.5f);
        flickerOtherStairs.Run();
        yield return new WaitUntil(() => gameManager.playerRoomBlock.ID == 8);
        gameManager.taskManager.taskEightComplete = true;
        yield return new WaitUntil(() => gameManager.playerRoomBlock.ID == 9);

        DialogueEvent dialogueEventShesGone = new DialogueEvent(11);
        dialogueEventShesGone.Run();
        yield return new WaitUntil(() => dialogueEventShesGone.State == EventState.DONE);
        gameManager.taskManager.StartNewTask(9);
        gameManager.taskManager.ManuallyUpdateToUncompleted();
        DoorEvent doorEvent7 = new DoorEvent(6, false);
        doorEvent7.Run();
        RoomEvent roomEvent7 = new RoomEvent(4, 7, RoomEventType.SWITCH);
        roomEvent7.Run();
        DoorEvent doorEvent8 = new DoorEvent(0, true);
        doorEvent8.Run();
        yield return new WaitForSeconds(0.5f);
        LightEvent flickerAttic = new LightEvent(0, LightEventType.FLICKER);
        flickerAttic.Run();
        yield return new WaitForSeconds(0.5f);
        flickerAttic.Run();
        yield return new WaitUntil(() => gameManager.playerRoomBlock.ID == 0);
        yield return new WaitForSeconds(0.2f);
        gameManager.taskManager.taskNineComplete = true;

        state = SequenceState.DONE;
    }
}

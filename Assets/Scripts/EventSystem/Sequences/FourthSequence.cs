using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthSequence : Sequence
{
    public FourthSequence()
    {
        ID = 3;
        state = SequenceState.WAITING;
        gameManager = GameManager.Instance;
    }

    public override bool IsTriggered()
    {
        gameManager = GameManager.Instance;

        if (gameManager.playerRoomBlock != null && gameManager.sequenceManager.sequences.Peek() == this && gameManager.taskManager.tasks[9].isCompleted)
            return true;

        return false;
    }

    public override IEnumerator Run()
    {
        Debug.Log("RUN FOURTH SEQUENCE");
        yield return new WaitForSeconds(2f);
        
        DialogueEvent tryFuseboxDialogue = new DialogueEvent(12);
        tryFuseboxDialogue.Run(); // Turn on the fusebox
        gameManager.houseManager.ResetRooms();
        gameManager.houseManager.ActivateAllDoors();
        yield return new WaitUntil(() => tryFuseboxDialogue.State == EventState.DONE);
        LightEvent turnOnSlowAllEvent = new LightEvent(0, LightEventType.ALLONSLOW);
        turnOnSlowAllEvent.Run();
        gameManager.houseManager.automaticLights = true;
        LightEvent turnOnAttic = new LightEvent(0, LightEventType.SLOWON);
        turnOnAttic.Run();
        gameManager.player.flashLight.TurnOffFlashLight();
        
        DialogueEvent succesFuseboxDialogue = new DialogueEvent(13);
        succesFuseboxDialogue.Run(); // You did it, time to look for grandma
        yield return new WaitUntil(() => succesFuseboxDialogue.State == EventState.DONE);
        gameManager.taskManager.StartNewTask(10); // Search the house and the front yard
        gameManager.taskManager.ManuallyUpdateToUncompleted();
        yield return new WaitUntil(() => gameManager.playerRoomBlock == null);
        yield return new WaitForSeconds(2f);
        gameManager.taskManager.taskTenComplete = true;

        DialogueEvent grandmaIsLoes = new DialogueEvent(14);
        grandmaIsLoes.Run(); // Give up searching grandma, see missed calls from mom, get back inside for wifi connection
        yield return new WaitUntil(() => grandmaIsLoes.State == EventState.DONE);
        gameManager.taskManager.StartNewTask(11); // Go to the livingroom for better wifi connection
        yield return new WaitUntil(() => gameManager.playerRoomBlock != null && gameManager.playerRoomBlock.ID == 9);
        yield return new WaitForSeconds(0.2f);
        gameManager.taskManager.taskElevenComplete = true;

        DialogueEvent mommyBellen = new DialogueEvent(15);
        mommyBellen.Run(); // Call mom
        yield return new WaitUntil(() => mommyBellen.State == EventState.DONE);
        gameManager.houseManager.automaticLights = false;
        LightEvent turnAllOff = new LightEvent(0, LightEventType.ALLOFF);
        turnAllOff.Run();
        gameManager.houseManager.DeactivateAllDoors();

        DialogueEvent hellooo = new DialogueEvent(16);
        hellooo.Run(); // No longer wifi, can't call with mom no longer, grandma responds from the dark
        yield return new WaitUntil(() => hellooo.State == EventState.DONE);
        gameManager.grandma.transform.Rotate(0, 0, -90);
        gameManager.grandma.transform.position = new Vector3(2.1f, -2.71f, 0f);
        gameManager.grandma.SetActive(true);
        
        DialogueEvent panic3000 = new DialogueEvent(17);
        panic3000.Run(); // Panic, door is gone
        gameManager.taskManager.StartNewTask(12); // Turn on flashlight with F
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F) == true);
        gameManager.taskManager.taskTwelveComplete = true;
        gameManager.player.flashLight.enabled = true;
        gameManager.player.flashLight.TurnOnFlashLight();

        //LightEvent flickerLivingroom = new LightEvent(9, LightEventType.FLICKER);
        //flickerLivingroom.Run();
        //yield return new WaitForSeconds(0.5f);
        //flickerLivingroom.Run();

        DialogueEvent scaryGranny = new DialogueEvent(18);
        scaryGranny.Run(); // Grandma tells you you forgot to eat your cookie, you scream
        yield return new WaitUntil(() => scaryGranny.State == EventState.DONE);
        gameManager.player.flashLight.TurnOffFlashLight();
        gameManager.player.flashLight.enabled = false;
        yield return new WaitForSeconds(3f);

        gameManager.GameIsOver();
    }
}

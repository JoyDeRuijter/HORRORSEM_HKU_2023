using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEvent : Event
{
    private HouseManager houseManager = GameManager.Instance.houseManager;
    private LightEventType eventType;

    // The ID is the index of the room that needs the light altered, the lightEventType determines what should happen with it.
    public LightEvent(int _id, LightEventType _eventType) : base(_id) => eventType = _eventType;

    public override void Run()
    {
        base.Run();
        Debug.Log("Running lightEvent " + ID + " with eventType: " + eventType.ToString());
        //dialogueManager.RunDialogueSequence(dialogueManager.dialogueSequences[ID], this);

        //TODO SwitchCase for all lightEventTypes, running methods from housemanager;
    }
}

public enum LightEventType { ON, OFF, SLOWON, FLICKER, ALLON, ALLOFF}
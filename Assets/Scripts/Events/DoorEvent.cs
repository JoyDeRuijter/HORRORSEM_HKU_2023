using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : Event
{
    private HouseManager houseManager = GameManager.Instance.houseManager;
    private bool shouldActivate;

    // The ID is the index of the door in doors from the housemanager, the boolean determines whether it should be activated or de-activated
    public DoorEvent(int _id, bool _shouldActivate) : base(_id) => shouldActivate = _shouldActivate;

    public override void Run()
    {
        base.Run();
        Debug.Log("Running doorEvent with " + ID + " and activate = " + shouldActivate);
        
        if (shouldActivate)
            houseManager.ActivateDoor(ID);
        else
            houseManager.DeactivateDoor(ID);
    }
}

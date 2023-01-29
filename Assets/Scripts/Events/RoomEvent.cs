using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEvent : Event
{
    private HouseManager houseManager = GameManager.Instance.houseManager;
    private int ID2;
    // The two ID's are the indexes of the roomblocks that are supposed to switch roomsets
    public RoomEvent(int _id, int _id2) : base(_id) => ID2 = _id2;

    public override void Run()
    {
        base.Run();
        Debug.Log("Running roomEvent with " + ID + " and " + ID2);
        houseManager.SwitchRooms(ID, ID2);
    }
}
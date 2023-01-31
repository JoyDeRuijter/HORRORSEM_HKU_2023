using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEvent : Event
{
    private HouseManager houseManager = GameManager.Instance.houseManager;
    private int ID2;
    private RoomEventType eventType;
    // The two ID's are the indexes of the roomblocks that are supposed to switch roomsets
    public RoomEvent(int _id, int _id2, RoomEventType _eventType) : base(_id)
    {
        ID2 = _id2;
        eventType= _eventType;
    }

    public override void Run()
    {
        base.Run();

        switch (eventType)
        {
            case RoomEventType.SWITCH:
                Debug.Log("Running roomEvent switch with " + ID + " and " + ID2);
                houseManager.SwitchRooms(ID, ID2);
                break;
            case RoomEventType.MAKENORMAL:
                Debug.Log("Running roomEvent makeNormal with " + ID);
                houseManager.TurnRoomNormal(ID);
                break;
            case RoomEventType.MAKESCARY:
                houseManager.TurnRoomScary(ID);
                break;
            case RoomEventType.MAKEALLNORMAL:
                houseManager.TurnAllRoomsNormal();
                break;
            case RoomEventType.MAKEALLSCARY:
                houseManager.TurnAllRoomsScary();
                break;
        }
    }
}

public enum RoomEventType { SWITCH, MAKENORMAL, MAKESCARY, MAKEALLNORMAL, MAKEALLSCARY}
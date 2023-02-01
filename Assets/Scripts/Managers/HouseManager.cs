using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    [HideInInspector] public bool automaticLights;

    [SerializeField] private RoomBlock[] roomBlocks = new RoomBlock[11];
    [SerializeField] private Door[] doors = new Door[7];
    
    private RoomSet[] roomSets = new RoomSet[11];
    private Door lastDisabledDoor;
    private RoomBlock lastRoomBlock;

    private void Awake()
    {
        //TurnOnAllLights();
        TurnOffAllLights();
        SaveOriginalRoomsets();
    }

    private void Start()
    {
        automaticLights = true;
    }

    private void Update()
    {
        // should fix this somehow
        if (automaticLights)
            AutomateLights(new List<int>() {0, 1, 2, 3, 4, 5, 6 });
    }

    private void SaveOriginalRoomsets()
    {
        for (int i = 0; i < roomBlocks.Length; i++)
        {
            roomSets[i] = roomBlocks[i].roomSet;
        }
    }

    // Method that turn all the rooms to normal sprites
    public void TurnAllRoomsNormal()
    {
        foreach (RoomBlock rb in roomBlocks)
            rb.isScary = false;
    }

    public void TurnAllRoomsScary()
    {
        foreach (RoomBlock rb in roomBlocks)
            rb.isScary = true;
    }

    public void TurnRoomNormal(int _roomIndex)
    {
        roomBlocks[_roomIndex].isScary = false;
    }

    public void TurnRoomScary(int _roomIndex)
    {
        roomBlocks[_roomIndex].isScary = true;
    }

    public void SwitchRooms(int _roomIndex1, int _roomIndex2)
    {
        RoomSet firstSet = roomBlocks[_roomIndex1].roomSet;
        RoomSet secondSet = roomBlocks[_roomIndex2].roomSet;

        roomBlocks[_roomIndex1].roomSet = secondSet;
        roomBlocks[_roomIndex2].roomSet = firstSet;
    }

    public void ResetRooms()
    {
        for (int i = 0; i < roomBlocks.Length; i++)
        {
            roomBlocks[i].roomSet = roomSets[i];
        }
    }

    public void ActivateDoor(int _doorIndex)
    { 
        if (!doors[_doorIndex].isActive)
            doors[_doorIndex].isActive = true;
    }

    public void DeactivateDoor(int _doorIndex)
    { 
        if (doors[_doorIndex].isActive)
            doors[_doorIndex].isActive = false;
    }

    public void ActivateAllDoors()
    { 
        foreach(Door d in doors)
            d.isActive = true;
    }

    public void DeactivateAllDoors()
    {
        foreach (Door d in doors)
            d.isActive = false;
    }

    // Test method that disables a random door and re-ables the last disabled door
    private void TestDisableEnableDoors()
    {
        int randomIndex = Random.Range(0, doors.Length);

        if (lastDisabledDoor != null)
            lastDisabledDoor.isActive = true;

        if (doors[randomIndex].isActive)
        {
            doors[randomIndex].isActive = false;
            lastDisabledDoor = doors[randomIndex];
        }
        else
        {
            if (randomIndex + 1 < doors.Length)
            {
                doors[randomIndex + 1].isActive = false;
                lastDisabledDoor = doors[randomIndex + 1];
            }
            else
            {
                doors[0].isActive = false;
                lastDisabledDoor = doors[0];
            }
        }
    }

    public void FlickerRoomLight(int _index)
    {
        StartCoroutine(roomBlocks[_index].FlickerLight());
    }

    public void TurnOnAllLights()
    { 
        foreach(RoomBlock rb in roomBlocks)
            rb.TurnLightOn();
    }

    public void TurnOnAllLightsSlow()
    {
        for (int i = 7; i < roomBlocks.Length; i++)
            roomBlocks[i].TurnLightOnSlow();
    }

    public void TurnOffAllLights()
    { 
        foreach(RoomBlock rb in roomBlocks)
            rb.TurnLightOff();
    }

    public void TurnOnLight(int _index)
    {
        roomBlocks[_index].TurnLightOn();
    }

    public void TurnOnLightSlow(int _index)
    {
        roomBlocks[_index].TurnLightOnSlow();
    }

    public void TurnOffLight(int _index)
    {
        roomBlocks[_index].TurnLightOff();
    }


    // Returns the roomblock the player is currently in
    public RoomBlock currentPlayerPosition()
    {
        Renderer playerRenderer = GameManager.Instance.player.gameObject.GetComponent<Renderer>();
        
        for (int i = 0; i < roomBlocks.Length; i++)
        { 
            if (playerRenderer.bounds.Intersects(roomBlocks[i].gameObject.GetComponent<Renderer>().bounds)) 
                return roomBlocks[i];
        }

        return null;
    }

    // Returns the roomblock below the given roomblock
    public RoomBlock RoomBelowRoom(RoomBlock _currentRoomBlock, Player _player)
    {
        int currentIndex = 100;
        for (int i = 0; i < roomBlocks.Length; i++)
        {
            if (roomBlocks[i] == _currentRoomBlock)
                currentIndex = i;
        }

        switch (currentIndex)
        {
            case 0: return roomBlocks[1];
            case 1: return roomBlocks[3];
            case 2: return roomBlocks[RoomIndexBelowStudio(_player)];
            case 3: return roomBlocks[7];
            case 4: return roomBlocks[8];
            case 5: return roomBlocks[9];
            case 6: return roomBlocks[10];
            default : return null;     
        }
    }

    // Returns the index of the room below the studio where the player is above
    private int RoomIndexBelowStudio(Player _player)
    {
        Vector2 pos = _player.gameObject.transform.position;

        if (pos.x >= 3.2f)
            return 6;
        else if (pos.x >= 0f && pos.x < 3.2f)
            return 5;
        else
            return 4;
    }

    private void AutomateLights(List<int> _roomIndexes)
    {
        if (lastRoomBlock == null && currentPlayerPosition() != null)
            lastRoomBlock = currentPlayerPosition();

        if (currentPlayerPosition() != lastRoomBlock && currentPlayerPosition() != null)
        {
            if (_roomIndexes.Contains(currentPlayerPosition().ID))
                currentPlayerPosition().TurnLightOnSlow();
            if (_roomIndexes.Contains(lastRoomBlock.ID))
                lastRoomBlock.TurnLightOff();

            lastRoomBlock = currentPlayerPosition();
        }
    }
}

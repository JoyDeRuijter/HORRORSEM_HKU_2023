using UnityEngine;

public class HouseManager : MonoBehaviour
{
    [SerializeField] private RoomBlock[] roomBlocks = new RoomBlock[11];
    [SerializeField] private Door[] doors = new Door[7];

    private Door lastDisabledDoor;

    private void Update()
    {
        // Temporary test functionality
        if(Input.GetKeyDown(KeyCode.F))
            TestLightsOnOff();

        // Temporary test functionality
        if (Input.GetKeyDown(KeyCode.G))
            TestSwitchRooms();

        // Temporary test functionality
        if (Input.GetKeyDown(KeyCode.R))
            TestDisableEnableDoors();
    }

    // Test method that turn all the lights on or off
    private void TestLightsOnOff()
    { 
        foreach(RoomBlock rb in roomBlocks)
            rb.isDark = !rb.isDark;
    }

    // Test method that switches 2 random rooms
    private void TestSwitchRooms()
    { 
        int i = Random.Range(3, roomBlocks.Length);
        int j = Random.Range(3, roomBlocks.Length);
        int compareValue = j + 1;

        if (i == j && compareValue >= roomBlocks.Length)
            j = 3;
        else if (i == j)
            j++;

        RoomSet firstSet = roomBlocks[i].roomSet;
        RoomSet secondSet = roomBlocks[j].roomSet;

        roomBlocks[i].roomSet = secondSet;
        roomBlocks[j].roomSet = firstSet;
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
}

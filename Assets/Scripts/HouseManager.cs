using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    [SerializeField] private RoomBlock[] roomBlocks = new RoomBlock[11];

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
            TestLightsOnOff();

        if (Input.GetKeyDown(KeyCode.S))
            TestSwitchRooms();
    }

    private void TestLightsOnOff()
    { 
        foreach(RoomBlock rb in roomBlocks)
            rb.isDark = !rb.isDark;
    }

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
}

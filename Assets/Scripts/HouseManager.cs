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
        int i = Random.Range(3, roomBlocks.Length - 1);
        int j = Random.Range(3, roomBlocks.Length - 1);

        if (i == j && j++ >= roomBlocks.Length)
            j = 3;
        else if (i == j)
            j++;

        RoomSet firstSet = roomBlocks[i].roomSet;
        RoomSet secondSet = roomBlocks[j].roomSet;

        roomBlocks[i].roomSet = secondSet;
        roomBlocks[j].roomSet = firstSet;
    }
}

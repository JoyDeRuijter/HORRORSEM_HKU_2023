using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Player player;
    public HouseManager houseManager;
    public string playerRoomPosition;

    private RoomBlock playerRoomBlock;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Update()
    {
        if (houseManager.currentPlayerPosition() == null)
        {
            playerRoomPosition = "Player is not in a room";
            playerRoomBlock = null;
        }
        else
        { 
            playerRoomPosition = houseManager.currentPlayerPosition().roomSet.setName;
            playerRoomBlock = houseManager.currentPlayerPosition();
        }

        if (Input.GetKeyDown(KeyCode.Space) && playerRoomBlock.roomSet.hasStairsOrLadder)
        { 
            StartCoroutine(player.UseStairsOrLadder(playerRoomBlock.gameObject, true, playerRoomBlock.roomSet.lowPositionPlayerAsChild, playerRoomBlock.roomSet.highPositionPlayerAsChild));
            Debug.Log("Pressed Space and is in room with stairs or ladder");
        }

    }
}

using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public Player player;
    public GameObject grandma;
    public HouseManager houseManager;
    public DialogueManager dialogueManager;
    public SequenceManager sequenceManager;
    public TaskManager taskManager;

    public RoomBlock playerRoomBlock;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        dialogueManager = GetComponent<DialogueManager>();
    }

    private void Start()
    {
        StartHouse();
        taskManager.StartNewTask(0);
    }

    private void Update()
    {
        CheckPlayerPosition();
        HandleStairsAndLadderInput();
    }

    private void StartHouse()
    {

        List<Event> startEvents = new List<Event>()
        {
            new RoomEvent(0, 0, RoomEventType.MAKEALLNORMAL){ },
            new LightEvent(0, LightEventType.ALLOFF){ },
            new LightEvent(7, LightEventType.ON){ },
            new LightEvent(8, LightEventType.ON){ },
            new LightEvent(9, LightEventType.ON){ },
            new LightEvent(10, LightEventType.ON){ }
        };

        foreach(Event e in startEvents)
            e.Run();
    }

    // Saves the current roomblock the player is in
    private void CheckPlayerPosition()
    {
        RoomBlock previousRoomBlock = playerRoomBlock;

        if (houseManager.currentPlayerPosition() == null && previousRoomBlock != null)
        { 
            playerRoomBlock = null;
            previousRoomBlock.playerIsHere = false;
        }
        else if (houseManager.currentPlayerPosition() != null && houseManager.currentPlayerPosition() != previousRoomBlock)
        { 
            playerRoomBlock = houseManager.currentPlayerPosition();
            playerRoomBlock.playerIsHere = true;
            if(previousRoomBlock != null)
                previousRoomBlock.playerIsHere = false;
        }
    }

    // Handles the input of the input for using the stairs and ladder
    private void HandleStairsAndLadderInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && playerRoomBlock.roomSet.hasStairsOrLadder && !player.isUsingStairsOrLadder)
            StartCoroutine(player.UseStairsOrLadder(playerRoomBlock.gameObject, true, playerRoomBlock.roomSet.lowPositionPlayerAsChild, playerRoomBlock.roomSet.highPositionPlayerAsChild));

        else if (Input.GetKeyDown(KeyCode.S)
                 && houseManager.RoomBelowRoom(playerRoomBlock, player) != null
                 && houseManager.RoomBelowRoom(playerRoomBlock, player).roomSet.hasStairsOrLadder
                 && !player.isUsingStairsOrLadder)
        {
            RoomBlock rb = houseManager.RoomBelowRoom(playerRoomBlock, player);
            StartCoroutine(player.UseStairsOrLadder(rb.gameObject, false, rb.roomSet.lowPositionPlayerAsChild, rb.roomSet.highPositionPlayerAsChild));
        }
    }

    //// Update the taskbar image because there seems to be a problem with that within the taskmanager
    //private void UpdateTaskbarImage()
    //{
    //    if (taskManager.currentTask.isCompleted && taskManager.image.sprite != taskManager.completedSprite)
    //        taskManager.ManuallyUpdateToCompleted();
    //    else if (!taskManager.currentTask.isCompleted && taskManager.image.sprite != taskManager.uncompletedSprite)
    //        taskManager.ManuallyUpdateToUncompleted();
    //}
}

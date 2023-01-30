using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public Player player;
    public HouseManager houseManager;
    public DialogueManager dialogueManager;

    public RoomBlock playerRoomBlock;
    DialogueEvent testEvent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        dialogueManager = GetComponent<DialogueManager>();

        //testEvent = new DialogueEvent(0);
    }

    private void Start()
    {
        //testEvent.Run();
    }

    private void Update()
    {
        CheckPlayerPosition();
        HandleStairsAndLadderInput();    
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
}

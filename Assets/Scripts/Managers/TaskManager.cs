using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    [Header("Task UI")]
    [SerializeField] private GameObject taskBar;
    [SerializeField] private TMP_Text textLabel;
    public Image image;

    [Space(10)]
    [Header("(un)completed sprites")]
    public Sprite completedSprite;
    public Sprite uncompletedSprite;

    [Space(10)]
    [Header("Tasks")]
    public Task[] tasks;

    [HideInInspector] public Task currentTask;
    [SerializeField] private GameManager gameManager;
    public int currentTaskIndex = 0;

    [HideInInspector] public bool taskOneComplete = false;
    [HideInInspector] public bool taskFourComplete = false;


    private void Update()
    {
        StartCoroutine(CheckIfTaskIsCompleted());
    }

    private void ShowTaskBar()
    {
        if (taskBar.activeSelf == true)
            return;
        taskBar.SetActive(true);
    }

    public void HideTaskBar() 
    {
        if (taskBar.activeSelf == false)
            return;
        taskBar.SetActive(false); 
    }

    public void ManuallyUpdateToUncompleted()
    {
        currentTask.isCompleted = false;
        image.sprite = uncompletedSprite;
    }

    public IEnumerator CompleteTask(bool _shouldClose)
    {
        if (currentTask.isCompleted)
            yield break;
        Debug.Log("COMPLETED TASK WITH INDEX: " + currentTaskIndex);
        image.sprite = completedSprite;
        currentTask.isCompleted = true;
        if (_shouldClose)
        { 
            yield return new WaitForSeconds(3f);
            HideTaskBar();
        }
    }

    public void StartNewTask(int _taskIndex)
    {
        Debug.Log("should start new task with taskindex: " + _taskIndex);
        ShowTaskBar();
        currentTaskIndex = _taskIndex;
        currentTask = tasks[_taskIndex];
        currentTask.isCompleted = false;
        image.sprite = null;
        image.sprite = uncompletedSprite;
        textLabel.text = currentTask.taskDescription;
        Debug.Log("should show new task with taskindex: " + _taskIndex);
    }

    private IEnumerator CheckIfTaskIsCompleted()
    {
        switch (currentTaskIndex)
        { 
            case 0:
                if (gameManager.playerRoomBlock != null && gameManager.playerRoomBlock.ID == 10)
                    StartCoroutine(CompleteTask(true));
                break;
            case 1:
                if (taskOneComplete)
                    StartCoroutine(CompleteTask(false));
                break;
            case 2:
                if (gameManager.playerRoomBlock != null && gameManager.playerRoomBlock.ID == 9)
                    StartCoroutine(CompleteTask(true));
                break;
            case 3:
                if (gameManager.playerRoomBlock != null && gameManager.playerRoomBlock.ID == 7)
                    StartCoroutine(CompleteTask(true));
                break;
            case 4:
                if (gameManager.playerRoomBlock != null && gameManager.playerRoomBlock.ID == 9 && taskFourComplete)
                    StartCoroutine(CompleteTask(true));
                break;
            default:
                yield break;
        
        }
    }
}
